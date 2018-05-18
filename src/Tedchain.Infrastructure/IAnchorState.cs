// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Provides functionality for creating a database anchor.
    /// </summary>
    public interface IAnchorState
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task Initialize();

        /// <summary>
        /// Gets the last known anchor.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<LedgerAnchor> GetLastAnchor();

        /// <summary>
        /// Marks the anchor as successfully recorded in the anchoring medium.
        /// </summary>
        /// <param name="anchor">The anchor to commit.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task CommitAnchor(LedgerAnchor anchor);
    }
}
