// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tedchain.Infrastructure;

namespace Tedchain.Server.Models
{
    public static class ConfigurationParser
    {
        public static ILogger CreateLogger(IServiceProvider serviceProvider)
        {
            return new DateLogger(serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("General"));
        }

        public static Task<Func<IServiceProvider, IStorageEngine>> CreateStorageEngine(IServiceProvider serviceProvider)
        {
            return DependencyResolver<IStorageEngine>.Create(serviceProvider, "storage");
        }

        public static ILedgerQueries CreateLedgerQueries(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IStorageEngine>() as ILedgerQueries;
        }

        public static ILedgerIndexes CreateLedgerIndexes(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IStorageEngine>() as ILedgerIndexes;
        }

        public static Task<Func<IServiceProvider, IAnchorState>> CreateAnchorState(IServiceProvider serviceProvider)
        {
            return DependencyResolver<IAnchorState>.Create(serviceProvider, "anchoring:storage");
        }

        public static Task<Func<IServiceProvider, IAnchorRecorder>> CreateAnchorRecorder(IServiceProvider serviceProvider)
        {
            return DependencyResolver<IAnchorRecorder>.Create(serviceProvider, "anchoring");
        }

        public static LedgerAnchorWorker CreateLedgerAnchorWorker(IServiceProvider serviceProvider)
        {
            return new LedgerAnchorWorker(serviceProvider);
        }

        public static Task<Func<IServiceProvider, IMutationValidator>> CreateRulesValidator(IServiceProvider serviceProvider)
        {
            return DependencyResolver<IMutationValidator>.Create(serviceProvider, "validator_mode:validator");
        }

        public static GlobalSettings CreateGlobalSettings(IServiceProvider serviceProvider)
        {
            string instanceSeed = serviceProvider.GetService<IConfiguration>().GetSection("validator_mode")["instance_seed"];

            ByteString validNamespace;
            if (string.IsNullOrEmpty(instanceSeed))
            {
                serviceProvider.GetService<ILogger>().LogWarning(
                    $"No instance seed is configured, this instance is not able to validate transactions");
                validNamespace = null;
            }
            else
            {
                validNamespace = new ByteString(MessageSerializer.ComputeHash(Encoding.UTF8.GetBytes(instanceSeed)).Take(8).ToArray());
            }

            return new GlobalSettings(validNamespace);
        }

        public static TransactionValidator CreateTransactionValidator(IServiceProvider serviceProvider)
        {
            IMutationValidator rulesValidator = serviceProvider.GetService<IMutationValidator>();

            if (rulesValidator == null)
            {
                return null;
            }
            else
            {
                GlobalSettings globalSettings = serviceProvider.GetService<GlobalSettings>();

                if (globalSettings.Namespace == null)
                    return null;
                else
                    return new TransactionValidator(serviceProvider.GetRequiredService<IStorageEngine>(), rulesValidator, globalSettings.Namespace);
            }
        }

        public static TransactionStreamSubscriber CreateStreamSubscriber(IServiceProvider serviceProvider)
        {
            ILogger logger = serviceProvider.GetService<ILogger>();

            if (serviceProvider.GetService<IMutationValidator>() != null)
            {
                logger.LogInformation("Stream subscriber disabled");
                return null;
            }
            else
            {
                IConfiguration observerMode = serviceProvider.GetService<IConfiguration>().GetSection("observer_mode");

                string upstreamUrl = observerMode["upstream_url"];

                if (string.IsNullOrEmpty(upstreamUrl))
                    throw new InvalidOperationException("Observer mode is enabled but no upstream URL has been specified.");

                logger.LogInformation("Current mode: Observer mode");
                logger.LogInformation("Upstream URL: {0}", upstreamUrl);

                return new TransactionStreamSubscriber(new Uri(upstreamUrl), serviceProvider);
            }
        }
    }
}
