// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents an observable collection constructed through polling.
    /// </summary>
    public class PollingObservable : IObservable<ByteString>
    {
        private readonly Func<ByteString, Task<IReadOnlyList<ByteString>>> query;
        private readonly ByteString from;

        public PollingObservable(ByteString from, Func<ByteString, Task<IReadOnlyList<ByteString>>> query)
        {
            this.from = from;
            this.query = query;
        }

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications
        /// before the provider has finished sending them.</returns>
        public IDisposable Subscribe(IObserver<ByteString> observer)
        {
            Subscription subscription = new Subscription(this, observer);
            subscription.Start(observer);
            return subscription;
        }

        private class Subscription : IDisposable
        {
            private readonly PollingObservable parent;
            private readonly CancellationTokenSource cancel = new CancellationTokenSource();

            public Subscription(PollingObservable parent, IObserver<ByteString> observer)
            {
                this.parent = parent;
            }

            public async void Start(IObserver<ByteString> observer)
            {
                try
                {
                    ByteString cursor = parent.from;

                    while (!cancel.Token.IsCancellationRequested)
                    {
                        IReadOnlyList<ByteString> result = await parent.query(cursor);

                        ByteString lastRecord = null;
                        foreach (ByteString record in result)
                        {
                            observer.OnNext(record);
                            lastRecord = record;
                        }

                        if (lastRecord != null)
                            cursor = new ByteString(MessageSerializer.ComputeHash(lastRecord.ToByteArray()));

                        await Task.Delay(TimeSpan.FromSeconds(0.2));
                    }

                    observer.OnCompleted();
                }
                catch (Exception exception)
                {
                    observer.OnError(exception);
                }
            }

            public void Dispose()
            {
                cancel.Cancel();
            }
        }
    }
}
