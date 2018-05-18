// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    /// <summary>
    /// Represents the implicit permission layout where account names contain identities.
    /// Permissions are set for:
    /// - /p2pkh/[addr]/ (AccountModify and optionally AccountSpend and DataModify)
    /// </summary>
    public class P2pkhImplicitLayout : IPermissionsProvider
    {
        private readonly KeyEncoder keyEncoder;
        private readonly LedgerPath p2pkhAccountPath = LedgerPath.Parse("/p2pkh/");

        public P2pkhImplicitLayout(KeyEncoder keyEncoder)
        {
            this.keyEncoder = keyEncoder;
        }

        public Task<PermissionSet> GetPermissions(IReadOnlyList<SignatureEvidence> authentication, LedgerPath path, bool recursiveOnly, string recordName)
        {
            HashSet<string> identities = new HashSet<string>(authentication.Select(evidence => keyEncoder.GetPubKeyHash(evidence.PublicKey)), StringComparer.Ordinal);

            // Account /p2pkh/[addr]/
            if (p2pkhAccountPath.IsStrictParentOf(path)
                && path.Segments.Count == p2pkhAccountPath.Segments.Count + 1
                && keyEncoder.IsP2pkh(path.Segments[path.Segments.Count - 1]))
            {
                Access ownAccount = identities.Contains(path.Segments[path.Segments.Count - 1]) && recordName != DynamicPermissionLayout.AclResourceName
                    ? Access.Permit : Access.Unset;

                return Task.FromResult(new PermissionSet(
                    accountModify: Access.Permit,
                    accountCreate: Access.Permit,
                    accountSpend: ownAccount,
                    dataModify: ownAccount));
            }
            else
            {
                return Task.FromResult(new PermissionSet());
            }
        }
    }
}
