// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tedchain.SqlServer.Tests
{
    public class SqlServerStorageEngineBuilderTests
    {
        private readonly IConfigurationSection configuration =
            new ConfigurationRoot(new[] { new MemoryConfigurationProvider(new MemoryConfigurationSource() { InitialData = new Dictionary<string, string>() { ["config:connection_string"] = ConfigurationManager.GetSetting("sql_connection_string") } }) })
            .GetSection("config");

        [Fact]
        public void Name_Success()
        {
            Assert.Equal("MSSQL", new SqlServerStorageEngineBuilder().Name);
        }

        [Fact]
        public async Task Build_Success()
        {
            SqlServerStorageEngineBuilder builder = new SqlServerStorageEngineBuilder();

            await builder.Initialize(new ServiceCollection().BuildServiceProvider(), configuration);

            SqlServerLedger ledger = builder.Build(null);

            Assert.NotNull(ledger);
        }
    }
}
