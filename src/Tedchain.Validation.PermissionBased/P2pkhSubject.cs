// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Linq;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    /// <summary>
    /// Represents an implementation of the <see cref="IPermissionSubject"/> interface based on public key hashes.
    /// </summary>
    public class P2pkhSubject : IPermissionSubject
    {
        private readonly KeyEncoder keyEncoder;

        public P2pkhSubject(IEnumerable<string> addresses, int signaturesRequired, KeyEncoder keyEncoder)
        {
            this.Addresses = addresses.ToList().AsReadOnly();
            this.SignaturesRequired = signaturesRequired;
            this.keyEncoder = keyEncoder;
        }

        /// <summary>
        /// Gets the list of valid addresses.
        /// </summary>
        public IReadOnlyList<string> Addresses { get; }

        /// <summary>
        /// Gets the number of required signatures for a match.
        /// </summary>
        public int SignaturesRequired { get; }

        public bool IsMatch(IReadOnlyList<SignatureEvidence> authentication)
        {
            HashSet<string> identities = new HashSet<string>(authentication.Select(evidence => keyEncoder.GetPubKeyHash(evidence.PublicKey)), StringComparer.Ordinal);
            return Addresses.Count(address => identities.Contains(address)) >= SignaturesRequired;
        }
    }
}
