// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    public static class StorageEngineExtensions
    {
        public static async Task<Record> GetRecord(this IStorageEngine store, RecordKey key)
        {
            IReadOnlyList<Record> result = await store.GetRecords(new[] { key.ToBinary() });
            return result[0];
        }

        public static async Task<IReadOnlyDictionary<AccountKey, AccountStatus>> GetAccounts(this IStorageEngine store, IEnumerable<AccountKey> accounts)
        {
            IReadOnlyList<Record> records = await store.GetRecords(accounts.Select(account => account.Key.ToBinary()));

            return records.Select(record => AccountStatus.FromRecord(RecordKey.Parse(record.Key), record)).ToDictionary(account => account.AccountKey, account => account);
        }
    }
}
