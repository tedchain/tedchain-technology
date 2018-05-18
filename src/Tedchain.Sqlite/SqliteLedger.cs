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
    public class SqliteLedger : SqliteStorageEngine, ILedgerQueries, ILedgerIndexes
    {
        public SqliteLedger(string filename)
            : base(filename)
        { }

        public async Task<ByteString> GetTransaction(ByteString mutationHash)
        {
            IEnumerable<ByteString> transactions = await ExecuteAsync(@"
                    SELECT  RawData
                    FROM    Transactions
                    WHERE   MutationHash = @mutationHash",
               reader => new ByteString((byte[])reader.GetValue(0)),
               new Dictionary<string, object>()
               {
                   ["@mutationHash"] = mutationHash.ToByteArray()
               });

            return transactions.FirstOrDefault();
        }

        public async Task<IReadOnlyList<Record>> GetKeyStartingFrom(ByteString prefix)
        {
            byte[] from = prefix.ToByteArray();
            byte[] to = prefix.ToByteArray();

            if (to[to.Length - 1] < 255)
                to[to.Length - 1] += 1;

            return await ExecuteAsync(@"
                    SELECT  Key, Value, Version
                    FROM    Records
                    WHERE   Key >= @from AND Key < @to",
                reader => new Record(
                    new ByteString((byte[])reader.GetValue(0)),
                    reader.GetValue(1) == null ? ByteString.Empty : new ByteString((byte[])reader.GetValue(1)),
                    new ByteString((byte[])reader.GetValue(2))),
                new Dictionary<string, object>()
                {
                    ["@from"] = from,
                    ["@to"] = to
                });
        }

        public async Task<IReadOnlyList<ByteString>> GetRecordMutations(ByteString recordKey)
        {
            return await ExecuteAsync(@"
                    SELECT  MutationHash
                    FROM    RecordMutations
                    WHERE   RecordKey = @recordKey",
                reader => new ByteString((byte[])reader.GetValue(0)),
                new Dictionary<string, object>()
                {
                    ["@recordKey"] = recordKey.ToByteArray()
                });
        }

        protected override async Task AddTransaction(long transactionId, byte[] mutationHash, Mutation mutation)
        {
            foreach (Record record in mutation.Records)
            {
                RecordKey key = RecordKey.Parse(record.Key);

                await ExecuteAsync(@"
                        UPDATE  Records
                        SET     Type = @type,
                                Name = @name
                        WHERE   Key = @key",
                    new Dictionary<string, object>()
                    {
                        ["@key"] = record.Key.ToByteArray(),
                        ["@type"] = (int)key.RecordType,
                        ["@name"] = key.Name
                    });

                await ExecuteAsync(@"
                        INSERT INTO RecordMutations
                        (RecordKey, TransactionId, MutationHash)
                        VALUES (@recordKey, @transactionId, @mutationHash)",
                    new Dictionary<string, object>()
                    {
                        ["@recordKey"] = record.Key.ToByteArray(),
                        ["@transactionId"] = transactionId,
                        ["@mutationHash"] = mutationHash
                    });
            }
        }

        public async Task<IReadOnlyList<Record>> GetAllRecords(RecordType type, string name)
        {
            return await ExecuteAsync(@"
                    SELECT  Key, Value, Version
                    FROM    Records
                    WHERE   Name = @name AND Type = @type",
                reader => new Record(
                    new ByteString((byte[])reader.GetValue(0)),
                    reader.GetValue(1) == null ? ByteString.Empty : new ByteString((byte[])reader.GetValue(1)),
                    new ByteString((byte[])reader.GetValue(2))),
                new Dictionary<string, object>()
                {
                    ["@name"] = name,
                    ["@type"] = (byte)type
                });
        }
    }
}
