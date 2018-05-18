// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Linq;
using System.Threading.Tasks;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Sqlite.Tests
{
    public class SqliteAnchorStateTests
    {
        private readonly ByteString[] binaryData =
            Enumerable.Range(0, 10).Select(index => new ByteString(Enumerable.Range(0, 32).Select(i => (byte)index))).ToArray();

        private readonly SqliteAnchorState anchorBuilder;

        public SqliteAnchorStateTests()
        {
            this.anchorBuilder = new SqliteAnchorState(":memory:");
            this.anchorBuilder.Initialize().Wait();
            SqliteAnchorStateBuilder.InitializeTables(this.anchorBuilder.Connection).Wait();
        }

        [Fact]
        public async Task GetLastAnchor_Success()
        {
            await this.anchorBuilder.CommitAnchor(new LedgerAnchor(binaryData[0], binaryData[1], 100));
            await this.anchorBuilder.CommitAnchor(new LedgerAnchor(binaryData[2], binaryData[3], 101));

            LedgerAnchor anchor = await this.anchorBuilder.GetLastAnchor();

            Assert.Equal(binaryData[2], anchor.Position);
            Assert.Equal(binaryData[3], anchor.FullStoreHash);
            Assert.Equal(101, anchor.TransactionCount);
        }

        [Fact]
        public async Task GetLastAnchor_NoAnchor()
        {
            LedgerAnchor anchor = await this.anchorBuilder.GetLastAnchor();

            Assert.Null(anchor);
        }
    }
}
