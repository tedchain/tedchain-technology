// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain
{
    /// <summary>
    /// Represents an error caused by the attempt of modifying a record using the wrong base version.
    /// </summary>
    public class ConcurrentMutationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentMutationException"/> class.
        /// </summary>
        /// <param name="failedMutation">The failed record mutation.</param>
        public ConcurrentMutationException(Record failedMutation)
            : base($"Version '{failedMutation.Version}' of key '{failedMutation.Key}' no longer exists.")
        {
            this.FailedMutation = failedMutation;
        }

        /// <summary>
        /// Gets the failed record mutation.
        /// </summary>
        public Record FailedMutation { get; }
    }
}
