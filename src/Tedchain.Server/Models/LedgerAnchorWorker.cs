// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tedchain.Infrastructure;

namespace Tedchain.Server.Models
{
    public class LedgerAnchorWorker
    {
        private readonly IServiceProvider services;

        public LedgerAnchorWorker(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task Run(CancellationToken cancel)
        {
            IServiceScopeFactory scopeFactory = services.GetService<IServiceScopeFactory>();
            ILogger logger = services.GetRequiredService<ILogger>();

            while (!cancel.IsCancellationRequested)
            {
                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    IAnchorRecorder anchorRecorder = scope.ServiceProvider.GetService<IAnchorRecorder>();
                    IAnchorState anchorState = scope.ServiceProvider.GetService<IAnchorState>();

                    if (anchorRecorder == null || anchorState == null)
                    {
                        logger.LogInformation("Anchoring disabled");
                        return;
                    }

                    IStorageEngine storageEngine = scope.ServiceProvider.GetRequiredService<IStorageEngine>();

                    try
                    {
                        await storageEngine.Initialize();
                        await anchorState.Initialize();

                        AnchorBuilder anchorBuilder = new AnchorBuilder(storageEngine, anchorRecorder, anchorState);

                        while (!cancel.IsCancellationRequested)
                        {
                            LedgerAnchor anchor = await anchorBuilder.RecordAnchor();

                            if (anchor != null)
                                logger.LogInformation($"Recorded an anchor for {anchor.TransactionCount} transactions: {anchor.FullStoreHash.ToString()}");

                            await Task.Delay(TimeSpan.FromSeconds(10), cancel);
                        }
                    }
                    catch (Exception exception)
                    {
                        logger.LogError($"Error in the anchor worker:\r\n{exception}");

                        // Wait longer if an error occurred
                        await Task.Delay(TimeSpan.FromMinutes(1), cancel);
                    }
                }
            }
        }
    }
}
