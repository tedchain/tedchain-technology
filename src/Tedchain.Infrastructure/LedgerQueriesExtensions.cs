// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    public static class LedgerQueriesExtensions
    {
        public static async Task<IReadOnlyList<AccountStatus>> GetAccount(this ILedgerQueries queries, string account)
        {
            ByteString prefix = new ByteString(Encoding.UTF8.GetBytes(account + ":ACC:"));
            IReadOnlyList<Record> records = await queries.GetKeyStartingFrom(prefix);

            return records
                .Where(record => !record.Value.Equals(ByteString.Empty))
                .Select(record => AccountStatus.FromRecord(RecordKey.Parse(record.Key), record))
                .ToList()
                .AsReadOnly();
        }

        public static async Task<IReadOnlyList<Record>> GetSubaccounts(this ILedgerQueries queries, string rootAccount)
        {
            ByteString prefix = new ByteString(Encoding.UTF8.GetBytes(rootAccount));
            IReadOnlyList<Record> records = await queries.GetKeyStartingFrom(prefix);

            return records
                .Where(record => !record.Value.Equals(ByteString.Empty))
                .ToList()
                .AsReadOnly();
        }

        public static async Task<Record> GetRecordVersion(this ILedgerQueries queries, ByteString key, ByteString version)
        {
            if (version.Value.Count == 0)
            {
                return new Record(key, ByteString.Empty, ByteString.Empty);
            }
            else
            {
                ByteString rawTransaction = await queries.GetTransaction(version);

                if (rawTransaction == null)
                {
                    return null;
                }
                else
                {
                    Transaction transaction = MessageSerializer.DeserializeTransaction(rawTransaction);
                    Mutation mutation = MessageSerializer.DeserializeMutation(transaction.Mutation);

                    Record result = mutation.Records.FirstOrDefault(record => record.Key.Equals(key) && record.Value != null);

                    if (result == null)
                        return null;
                    else
                        return result;
                }
            }
        }
    }
}
