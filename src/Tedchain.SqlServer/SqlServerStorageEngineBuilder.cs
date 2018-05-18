// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tedchain.Infrastructure;

namespace Tedchain.SqlServer
{
    public class SqlServerStorageEngineBuilder : IComponentBuilder<SqlServerLedger>
    {
        private string connectionString;

        public string Name { get; } = "MSSQL";

        public SqlServerLedger Build(IServiceProvider serviceProvider)
        {
            return new SqlServerLedger(connectionString, 1, TimeSpan.FromSeconds(10));
        }

        public Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration)
        {
            this.connectionString = configuration["connection_string"];

            return Task.FromResult(0);
        }
    }
}
