// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain
{
    /// <summary>
    /// Represents a transaction affecting the data store.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="mutation">The binary representation of the <see cref="Mutation"/> applied by this transaction.</param>
        /// <param name="timestamp">The timestamp of the transaction.</param>
        /// <param name="transactionMetadata">The metadata associated with the transaction.</param>
        public Transaction(ByteString mutation, DateTime timestamp, ByteString transactionMetadata)
        {
            if (mutation == null)
                throw new ArgumentNullException(nameof(mutation));

            if (transactionMetadata == null)
                throw new ArgumentNullException(nameof(transactionMetadata));

            this.Mutation = mutation;
            this.Timestamp = timestamp;
            this.TransactionMetadata = transactionMetadata;
        }

        /// <summary>
        /// Gets the binary representation of the <see cref="Mutation"/> applied by this transaction.
        /// </summary>
        public ByteString Mutation { get; }

        /// <summary>
        /// Gets the timestamp of the transaction.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the metadata associated with the transaction.
        /// </summary>
        public ByteString TransactionMetadata { get; }
    }
}
