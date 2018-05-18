// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents the key of an account record.
    /// </summary>
    public class AccountKey : IEquatable<AccountKey>
    {
        public AccountKey(LedgerPath account, LedgerPath asset)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (asset == null)
                throw new ArgumentNullException(nameof(asset));

            this.Account = account;
            this.Asset = asset;
            this.Key = new RecordKey(RecordType.Account, account, asset.FullPath);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AccountKey"/> class from an account and asset.
        /// </summary>
        /// <param name="account">The account path.</param>
        /// <param name="asset">The asset path.</param>
        /// <returns>An instance of the <see cref="AccountKey"/> class representing the account and asset provided.</returns>
        public static AccountKey Parse(string account, string asset)
        {
            return new AccountKey(
                LedgerPath.Parse(account),
                LedgerPath.Parse(asset));
        }

        /// <summary>
        /// Gets the <see cref="LedgerPath"/> of the account that this instance represents.
        /// </summary>
        public LedgerPath Account { get; }

        /// <summary>
        /// Gets the <see cref="LedgerPath"/> of the asset that this instance represents.
        /// </summary>
        public LedgerPath Asset { get; }

        /// <summary>
        /// Gets the <see cref="RecordKey"/> equivalent to this instance.
        /// </summary>
        public RecordKey Key { get; }

        public bool Equals(AccountKey other)
        {
            if (other == null)
                return false;
            else
                return StringComparer.Ordinal.Equals(Key.ToString(), other.Key.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj is AccountKey)
                return this.Equals((AccountKey)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(ToString());
        }
    }
}
