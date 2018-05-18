// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Provides functionality for recording a database anchor.
    /// </summary>
    public interface IAnchorRecorder
    {
        /// <summary>
        /// Indicates whether this instance is ready to record a new database anchor.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<bool> CanRecordAnchor();

        /// <summary>
        /// Records a database anchor.
        /// </summary>
        /// <param name="anchor">The anchor to be recorded.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task RecordAnchor(LedgerAnchor anchor);
    }
}
