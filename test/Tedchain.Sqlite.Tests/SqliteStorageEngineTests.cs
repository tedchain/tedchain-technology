// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using Tedchain.Tests;

namespace Tedchain.Sqlite.Tests
{
    public class SqliteStorageEngineTests : BaseStorageEngineTests
    {
        public SqliteStorageEngineTests()
        {
            SqliteStorageEngine store = new SqliteStorageEngine(":memory:");
            store.Initialize().Wait();
            SqliteStorageEngineBuilder.InitializeTables(store.Connection).Wait();

            this.Store = store;
        }
    }
}
