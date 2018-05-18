// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tedchain.Server.Models
{
    public class TransactionStreamSubscriber
    {
        private readonly UriBuilder endpoint;
        private readonly IServiceProvider services;

        public TransactionStreamSubscriber(Uri endpoint, IServiceProvider services)
        {
            this.endpoint = new UriBuilder(endpoint);

            if (endpoint.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                this.endpoint.Scheme = "wss";
            else
                this.endpoint.Scheme = "ws";

            this.endpoint.Path = this.endpoint.Path.TrimEnd('/') + "/stream";
            this.services = services;
        }

        public async Task Subscribe(CancellationToken cancel)
        {
            byte[] buffer = new byte[1024 * 1024];
            ArraySegment<byte> segment = new ArraySegment<byte>(buffer);

            IServiceScopeFactory scopeFactory = services.GetService<IServiceScopeFactory>();
            ILogger logger = services.GetRequiredService<ILogger>();

            while (!cancel.IsCancellationRequested)
            {
                try
                {
                    using (IServiceScope scope = scopeFactory.CreateScope())
                    {
                        IStorageEngine storageEngine = scope.ServiceProvider.GetRequiredService<IStorageEngine>();
                        await storageEngine.Initialize();

                        ByteString currentRecord = await storageEngine.GetLastTransaction();

                        ClientWebSocket socket = new ClientWebSocket();

                        this.endpoint.Query = string.Format("from={0}", currentRecord.ToString());

                        logger.LogInformation("Connecting to {0}", this.endpoint.Uri);

                        await socket.ConnectAsync(this.endpoint.Uri, cancel);

                        while (true)
                        {
                            ByteString transaction;

                            using (MemoryStream stream = new MemoryStream(1024))
                            {
                                WebSocketReceiveResult result;

                                do
                                {
                                    result = await socket.ReceiveAsync(segment, cancel);
                                    if (result.MessageType == WebSocketMessageType.Close)
                                        break;

                                    stream.Write(segment.Array, segment.Offset, result.Count);

                                } while (!result.EndOfMessage);

                                stream.Seek(0, SeekOrigin.Begin);

                                using (BinaryReader reader = new BinaryReader(stream))
                                {
                                    transaction = new ByteString(reader.ReadBytes((int)stream.Length));
                                }
                            }

                            await storageEngine.AddTransactions(new[] { transaction });

                            currentRecord = new ByteString(MessageSerializer.ComputeHash(transaction.ToByteArray()));
                        }
                    }
                }
                catch (Exception exception)
                {
                    logger.LogError("Error in the stream subscriber: {0}", exception.ToString());
                }

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
