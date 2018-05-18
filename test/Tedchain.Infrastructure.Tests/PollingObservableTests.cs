// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tedchain.Infrastructure.Tests
{
    public class PollingObservableTests
    {
        [Fact]
        public async Task Subscribe_Success()
        {
            TestObserver observer = new TestObserver() { ExpectedValueCount = 4 };

            int current = 0;
            Func<ByteString, Task<IReadOnlyList<ByteString>>> query = delegate (ByteString start)
            {
                List<ByteString> result = new List<ByteString>();
                result.Add(new ByteString(new byte[] { (byte)current, 0 }));
                result.Add(new ByteString(new byte[] { (byte)current, 1 }));
                current++;

                return Task.FromResult<IReadOnlyList<ByteString>>(result);
            };

            IObservable<ByteString> stream = new PollingObservable(ByteString.Empty, query);
            using (stream.Subscribe(observer))
                await observer.Completed.Task;

            await observer.Disposed.Task;

            Assert.False(observer.Fail);
            Assert.Equal(4, observer.Values.Count);
            Assert.Equal(ByteString.Parse("0000"), observer.Values[0]);
            Assert.Equal(ByteString.Parse("0001"), observer.Values[1]);
            Assert.Equal(ByteString.Parse("0100"), observer.Values[2]);
            Assert.Equal(ByteString.Parse("0101"), observer.Values[3]);
        }

        [Fact]
        public async Task Subscribe_Error()
        {
            TestObserver observer = new TestObserver() { ExpectedValueCount = 1 };

            Func<ByteString, Task<IReadOnlyList<ByteString>>> query = delegate (ByteString start)
            {
                return Task.FromException<IReadOnlyList<ByteString>>(new ArithmeticException());
            };

            IObservable<ByteString> stream = new PollingObservable(ByteString.Empty, query);
            using (stream.Subscribe(observer))
                await observer.Completed.Task;

            await observer.Disposed.Task;

            Assert.True(observer.Fail);
            Assert.Equal(0, observer.Values.Count);
        }

        private class TestObserver : IObserver<ByteString>
        {
            public int ExpectedValueCount { get; set; }

            public TaskCompletionSource<int> Completed { get; } = new TaskCompletionSource<int>();

            public TaskCompletionSource<int> Disposed { get; } = new TaskCompletionSource<int>();

            public IList<ByteString> Values { get; } = new List<ByteString>();

            public bool Fail { get; set; }

            public void OnCompleted() => Disposed.SetResult(0);

            public void OnError(Exception error)
            {
                Fail = true;
                this.Completed.SetResult(0);
                Disposed.SetResult(0);
            }

            public void OnNext(ByteString value)
            {
                Values.Add(value);

                if (Values.Count == ExpectedValueCount)
                    this.Completed.SetResult(0);
            }
        }
    }
}
