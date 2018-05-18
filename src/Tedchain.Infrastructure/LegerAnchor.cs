// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a database anchor containing the cumulative hash of the data store.
    /// </summary>
    public class LedgerAnchor
    {
        public LedgerAnchor(ByteString position, ByteString fullStoreHash, long transactionCount)
        {
            Position = position;
            FullStoreHash = fullStoreHash;
            TransactionCount = transactionCount;
        }

        /// <summary>
        /// Gets the hash of the last transaction in the ledger in the current state.
        /// </summary>
        public ByteString Position { get; }

        /// <summary>
        /// Gets the cumulative hash of the ledger.
        /// </summary>
        public ByteString FullStoreHash { get; }

        /// <summary>
        /// Gets the total count of transactions in the ledger.
        /// </summary>
        public long TransactionCount { get; }
    }
}
