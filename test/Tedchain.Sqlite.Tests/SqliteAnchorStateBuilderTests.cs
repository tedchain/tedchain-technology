// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tedchain.Sqlite.Tests
{
    public class SqliteAnchorStateBuilderTests
    {
        private readonly IConfigurationSection configuration =
            new ConfigurationRoot(new[] { new MemoryConfigurationProvider(new MemoryConfigurationSource() { InitialData = new Dictionary<string, string>() { ["config:path"] = ":memory:" } }) })
            .GetSection("config");

        [Fact]
        public void Name_Success()
        {
            Assert.Equal("SQLite", new SqliteAnchorStateBuilder().Name);
        }

        [Fact]
        public async Task Build_Success()
        {
            SqliteAnchorStateBuilder builder = new SqliteAnchorStateBuilder();

            await builder.Initialize(new ServiceCollection().BuildServiceProvider(), configuration);

            SqliteAnchorState ledger = builder.Build(null);

            Assert.NotNull(ledger);
        }

        [Fact]
        public async Task InitializeTables_CallTwice()
        {
            SqliteAnchorStateBuilder builder = new SqliteAnchorStateBuilder();

            await builder.Initialize(new ServiceCollection().BuildServiceProvider(), configuration);

            SqliteAnchorState ledger = builder.Build(null);

            await SqliteAnchorStateBuilder.InitializeTables(ledger.Connection);
            await SqliteAnchorStateBuilder.InitializeTables(ledger.Connection);

            Assert.Equal(ConnectionState.Open, ledger.Connection.State);
        }
    }
}
