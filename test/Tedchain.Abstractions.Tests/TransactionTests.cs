// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using Xunit;

namespace Tedchain.Tests
{
    public class TransactionTests
    {
        private readonly ByteString[] binaryData =
            Enumerable.Range(0, 10).Select(index => new ByteString(Enumerable.Range(0, 32).Select(i => (byte)index))).ToArray();

        [Fact]
        public void Transaction_Success()
        {
            Transaction record = new Transaction(
                binaryData[0],
                new DateTime(1, 2, 3, 4, 5, 6),
                binaryData[1]);

            Assert.Equal(binaryData[0], record.Mutation);
            Assert.Equal(new DateTime(1, 2, 3, 4, 5, 6), record.Timestamp);
            Assert.Equal(binaryData[1], record.TransactionMetadata);
        }

        [Fact]
        public void Transaction_ArgumentNullException()
        {
            ArgumentNullException exception;

            exception = Assert.Throws<ArgumentNullException>(() => new Transaction(
                null,
                new DateTime(1, 2, 3, 4, 5, 6),
                binaryData[1]));
            Assert.Equal("mutation", exception.ParamName);

            exception = Assert.Throws<ArgumentNullException>(() => new Transaction(
                binaryData[0],
                new DateTime(1, 2, 3, 4, 5, 6),
                null));
            Assert.Equal("transactionMetadata", exception.ParamName);
        }
    }
}
