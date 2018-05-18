// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Tedchain.Infrastructure;

namespace Tedchain.Sqlite
{
    public class SqliteAnchorStateBuilder : IComponentBuilder<SqliteAnchorState>
    {
        private string filename;

        public string Name { get; } = "SQLite";

        public SqliteAnchorState Build(IServiceProvider serviceProvider)
        {
            return new SqliteAnchorState(filename);
        }

        public async Task Initialize(IServiceProvider serviceProvider, IConfigurationSection configuration)
        {
            filename = SqliteStorageEngineBuilder.GetPathOrDefault(serviceProvider, configuration["path"]);

            using (SqliteConnection connection = new SqliteConnection(new SqliteConnectionStringBuilder() { DataSource = filename }.ToString()))
            {
                await InitializeTables(connection);
            }
        }

        public static async Task InitializeTables(SqliteConnection connection)
        {
            await connection.OpenAsync();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Anchors
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Position BLOB UNIQUE,
                    FullLedgerHash BLOB,
                    TransactionCount INT,
                    AnchorId BLOB
                );";

            await command.ExecuteNonQueryAsync();
        }
    }
}
