// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using System.Text;
using Assert = Xunit.Assert;
using Fact = Xunit.FactAttribute;

namespace Tedchain.Infrastructure.Tests
{
    public class ParsedMutationTests
    {
        private readonly ByteString[] binaryData =
            Enumerable.Range(0, 10).Select(index => new ByteString(Enumerable.Range(0, 32).Select(i => (byte)index))).ToArray();

        [Fact]
        public void Parse_AccountMutations()
        {
            ParsedMutation result = Parse(new Record(
                SerializeString("/the/account/:ACC:/the/asset/"),
                SerializeInt(100),
                binaryData[3]));

            Assert.Equal(1, result.AccountMutations.Count);
            Assert.Equal(0, result.DataRecords.Count);
            Assert.Equal("/the/account/", result.AccountMutations[0].AccountKey.Account.FullPath);
            Assert.Equal("/the/asset/", result.AccountMutations[0].AccountKey.Asset.FullPath);
            Assert.Equal(100, result.AccountMutations[0].Balance);
            Assert.Equal(binaryData[3], result.AccountMutations[0].Version);
        }

        [Fact]
        public void Parse_Data()
        {
            ParsedMutation result = Parse(new Record(
                SerializeString("/aka/alias/:DATA:name"),
                ByteString.Parse("aabbccdd"),
                binaryData[3]));

            Assert.Equal(0, result.AccountMutations.Count);
            Assert.Equal(1, result.DataRecords.Count);
            Assert.Equal("/aka/alias/", result.DataRecords[0].Key.Path.FullPath);
            Assert.Equal(RecordType.Data, result.DataRecords[0].Key.RecordType);
            Assert.Equal("name", result.DataRecords[0].Key.Name);
            Assert.Equal(ByteString.Parse("aabbccdd"), result.DataRecords[0].Value);
        }

        [Fact]
        public void Parse_OptimisticConcurrency()
        {
            ParsedMutation result = Parse(new Record(
                SerializeString("/the/account/:ACC:/the/asset/"),
                null,
                binaryData[3]));

            Assert.Equal(0, result.AccountMutations.Count);
            Assert.Equal(0, result.DataRecords.Count);
        }

        [Fact]
        public void Parse_InvalidRecord()
        {
            TransactionInvalidException exception;

            // Invalid number of components
            exception = Assert.Throws<TransactionInvalidException>(() => Parse(new Record(
                SerializeString("/the/account/:ACC"),
                SerializeInt(100),
                binaryData[3])));
            Assert.Equal("NonCanonicalSerialization", exception.Reason);

            // Unknown record
            exception = Assert.Throws<TransactionInvalidException>(() => Parse(new Record(
                SerializeString("/the/asset/:INVALID:/other/path/"),
                SerializeString("Definition"),
                binaryData[3])));
            Assert.Equal("InvalidRecord", exception.Reason);

            // Invalid path
            exception = Assert.Throws<TransactionInvalidException>(() => Parse(new Record(
                SerializeString("the/account/:ACC:/the/asset/"),
                SerializeInt(100),
                binaryData[3])));
            Assert.Equal("InvalidPath", exception.Reason);

            exception = Assert.Throws<TransactionInvalidException>(() => Parse(new Record(
                SerializeString("/the/account/:ACC:the/asset/"),
                SerializeInt(100),
                binaryData[3])));
            Assert.Equal("InvalidPath", exception.Reason);

            // Invalid account balance
            exception = Assert.Throws<TransactionInvalidException>(() => Parse(new Record(
                SerializeString("/the/account/:ACC:/the/asset/"),
                SerializeString("01"),
                binaryData[3])));
            Assert.Equal("InvalidRecord", exception.Reason);
        }

        private ParsedMutation Parse(params Record[] records)
        {
            Mutation mutation = new Mutation(
                binaryData[1],
                records,
                binaryData[2]);

            return ParsedMutation.Parse(mutation);
        }

        private static ByteString SerializeInt(long value)
        {
            return new ByteString(BitConverter.GetBytes(value).Reverse());
        }

        private static ByteString SerializeString(string value)
        {
            return new ByteString(Encoding.UTF8.GetBytes(value));
        }
    }
}
