// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Data.SqlClient;
using Tedchain.Infrastructure.Tests;
using Xunit;

namespace Tedchain.SqlServer.Tests
{
    [Collection("SQL Server Tests")]
    public class SqlServerLedgerTests : BaseLedgerTests
    {
        public SqlServerLedgerTests()
        {
            SqlServerLedger engine = new SqlServerLedger(ConfigurationManager.GetSetting("sql_connection_string"), 1, TimeSpan.FromSeconds(10));
            engine.Initialize().Wait();

            SqlCommand command = engine.Connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM [Tedchain].[RecordMutations];
                DELETE FROM [Tedchain].[Records];
                DELETE FROM [Tedchain].[Transactions];
            ";

            command.ExecuteNonQuery();

            this.Engine = engine;
            this.Queries = engine;
            this.Indexes = engine;
        }
    }
}
