// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents the metadata object that can be attached to a transaction, and contains signatures for that transaction.
    /// </summary>
    public class TransactionMetadata
    {
        public TransactionMetadata(IEnumerable<SignatureEvidence> signatures)
        {
            this.Signatures = new ReadOnlyCollection<SignatureEvidence>(signatures.ToList());
        }

        /// <summary>
        /// Gets the list of signatures.
        /// </summary>
        public IReadOnlyList<SignatureEvidence> Signatures { get; }
    }
}
