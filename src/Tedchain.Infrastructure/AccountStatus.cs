// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a parsed account record.
    /// </summary>
    public class AccountStatus
    {
        public AccountStatus(AccountKey accountKey, long amount, ByteString version)
        {
            if (accountKey == null)
                throw new ArgumentNullException(nameof(accountKey));

            if (version == null)
                throw new ArgumentNullException(nameof(version));

            this.AccountKey = accountKey;
            this.Balance = amount;
            this.Version = version;
        }

        /// <summary>
        /// Gets the key of the record.
        /// </summary>
        public AccountKey AccountKey { get; }

        /// <summary>
        /// Gets the balance on the account.
        /// </summary>
        public long Balance { get; }

        /// <summary>
        /// Gets the version of the record.
        /// </summary>
        public ByteString Version { get; }

        /// <summary>
        /// Creates an instance of the <see cref="AccountStatus"/> class from an unparsed record.
        /// </summary>
        /// <param name="key">The key of the record.</param>
        /// <param name="record">The record to create the object from.</param>
        /// <returns>A new instance of the <see cref="AccountStatus"/> class.</returns>
        public static AccountStatus FromRecord(RecordKey key, Record record)
        {
            if (key.RecordType != RecordType.Account)
                throw new ArgumentOutOfRangeException(nameof(key));

            long amount;
            if (record.Value.Value.Count == 0)
                amount = 0;
            else if (record.Value.Value.Count == 8)
                amount = BitConverter.ToInt64(record.Value.Value.Reverse().ToArray(), 0);
            else
                throw new ArgumentOutOfRangeException(nameof(record));

            return new AccountStatus(new AccountKey(key.Path, LedgerPath.Parse(key.Name)), amount, record.Version);
        }
    }
}
