// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a set of query operations that can be performed against a transaction store.
    /// </summary>
    public interface ILedgerQueries
    {
        /// <summary>
        /// Returns all the record that have a key starting by the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix to query for.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Record>> GetKeyStartingFrom(ByteString prefix);

        /// <summary>
        /// Returns a transaction serialized as a <see cref="ByteString"/>, given its hash.
        /// </summary>
        /// <param name="mutationHash">The hash of the transaction.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<ByteString> GetTransaction(ByteString mutationHash);

        /// <summary>
        /// Returns a list of mutation hashes that have affected a given record.
        /// </summary>
        /// <param name="recordKey">The key of the record of which mutations are being retrieved.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IReadOnlyList<ByteString>> GetRecordMutations(ByteString recordKey);
    }
}
