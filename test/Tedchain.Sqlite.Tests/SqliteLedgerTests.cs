// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;
using Tedchain.Infrastructure.Tests;
using Xunit;

namespace Tedchain.Sqlite.Tests
{
    public class SqliteLedgerTests : BaseLedgerTests
    {
        public SqliteLedgerTests()
        {
            SqliteLedger store = new SqliteLedger(":memory:");
            store.Initialize().Wait();
            SqliteStorageEngineBuilder.InitializeTables(store.Connection).Wait();

            this.Engine = store;
            this.Queries = store;
            this.Indexes = store;
        }
    }
}
