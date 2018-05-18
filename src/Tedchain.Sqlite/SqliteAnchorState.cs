// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedchain.Infrastructure;

namespace Tedchain.Sqlite
{
    /// <summary>
    /// Persists information about the latest known anchor.
    /// </summary>
    public class SqliteAnchorState : SqliteBase, IAnchorState
    {
        public SqliteAnchorState(string filename)
            : base(filename)
        { }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task Initialize()
        {
            await Connection.OpenAsync();
        }

        /// <summary>
        /// Gets the last known anchor.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<LedgerAnchor> GetLastAnchor()
        {
            IEnumerable<LedgerAnchor> anchors = await ExecuteAsync(@"
                    SELECT  Position, FullLedgerHash, TransactionCount
                    FROM    Anchors
                    ORDER BY Id DESC
                    LIMIT 1",
                reader => new LedgerAnchor(
                    new ByteString((byte[])reader.GetValue(0)),
                    new ByteString((byte[])reader.GetValue(1)),
                    reader.GetInt64(2)),
                new Dictionary<string, object>());

            return anchors.FirstOrDefault();
        }

        /// <summary>
        /// Marks the anchor as successfully recorded in the anchoring medium.
        /// </summary>
        /// <param name="anchor">The anchor to commit.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CommitAnchor(LedgerAnchor anchor)
        {
            await ExecuteAsync(@"
                    INSERT INTO Anchors
                    (Position, FullLedgerHash, TransactionCount)
                    VALUES (@position, @fullLedgerHash, @transactionCount)",
                new Dictionary<string, object>()
                {
                    ["@position"] = anchor.Position.ToByteArray(),
                    ["@fullLedgerHash"] = anchor.FullStoreHash.ToByteArray(),
                    ["@transactionCount"] = anchor.TransactionCount
                });
        }
    }
}
