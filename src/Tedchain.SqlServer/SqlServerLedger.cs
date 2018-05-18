// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tedchain.Infrastructure;

namespace Tedchain.SqlServer
{
    public class SqlServerLedger : SqlServerStorageEngine, ILedgerQueries, ILedgerIndexes
    {
        private readonly int instanceId;

        public SqlServerLedger(string connectionString, int instanceId, TimeSpan commandTimeout)
            : base(connectionString, instanceId, commandTimeout)
        {
            this.instanceId = instanceId;
        }

        public async Task<IReadOnlyList<Record>> GetKeyStartingFrom(ByteString prefix)
        {
            byte[] from = prefix.ToByteArray();
            byte[] to = prefix.ToByteArray();
            to[to.Length - 1]++;

            return await ExecuteQuery<Record>(
                "EXEC [Tedchain].[GetRecordRange] @instance, @from, @to;",
                reader => new Record(new ByteString((byte[])reader[0]), new ByteString((byte[])reader[1]), new ByteString((byte[])reader[2])),
                new Dictionary<string, object>()
                {
                    ["instance"] = this.instanceId,
                    ["from"] = from,
                    ["to"] = to
                });
        }

        public async Task<IReadOnlyList<ByteString>> GetRecordMutations(ByteString recordKey)
        {
            return await ExecuteQuery<ByteString>(
                "EXEC [Tedchain].[GetRecordMutations] @instance, @recordKey;",
                reader => new ByteString((byte[])reader[0]),
                new Dictionary<string, object>()
                {
                    ["instance"] = this.instanceId,
                    ["recordKey"] = recordKey.ToByteArray()
                });
        }

        public async Task<ByteString> GetTransaction(ByteString mutationHash)
        {
            IReadOnlyList<ByteString> result = await ExecuteQuery<ByteString>(
                "EXEC [Tedchain].[GetTransaction] @instance, @mutationHash;",
                reader => new ByteString((byte[])reader[0]),
                new Dictionary<string, object>()
                {
                    ["instance"] = this.instanceId,
                    ["mutationHash"] = mutationHash.ToByteArray()
                });

            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        public async Task<IReadOnlyList<Record>> GetAllRecords(RecordType type, string name)
        {
            return await ExecuteQuery<Record>(
                "EXEC [Tedchain].[GetAllRecords] @instance, @recordType, @recordName;",
                reader => new Record(new ByteString((byte[])reader[0]), new ByteString((byte[])reader[1]), new ByteString((byte[])reader[2])),
                new Dictionary<string, object>()
                {
                    ["instance"] = this.instanceId,
                    ["recordType"] = (byte)type,
                    ["recordName"] = name
                });
        }

        protected override RecordKey ParseRecordKey(ByteString key)
        {
            return RecordKey.Parse(key);
        }
    }
}
