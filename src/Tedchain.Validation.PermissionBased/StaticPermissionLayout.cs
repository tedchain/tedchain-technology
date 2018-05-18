// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Threading.Tasks;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public class StaticPermissionLayout : IPermissionsProvider
    {
        private readonly IList<Acl> permissions;

        public StaticPermissionLayout(IList<Acl> permissions)
        {
            this.permissions = permissions;
        }

        public Task<PermissionSet> GetPermissions(IReadOnlyList<SignatureEvidence> authentication, LedgerPath path, bool recursiveOnly, string recordName)
        {
            PermissionSet currentPermissions = PermissionSet.Unset;

            foreach (Acl acl in permissions)
            {
                if (acl.IsMatch(authentication, path, recursiveOnly, recordName))
                    currentPermissions = currentPermissions.Add(acl.Permissions);
            }

            return Task.FromResult(currentPermissions);
        }
    }
}
