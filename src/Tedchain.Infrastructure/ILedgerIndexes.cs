// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a set of advanced operations that can be performed against a transaction store.
    /// </summary>
    public interface ILedgerIndexes
    {
        /// <summary>
        /// Returns all records with a particular name and type.
        /// </summary>
        /// <param name="type">The type of the records to return.</param>
        /// <param name="name">The name of the records to return.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IReadOnlyList<Record>> GetAllRecords(RecordType type, string name);
    }
}
