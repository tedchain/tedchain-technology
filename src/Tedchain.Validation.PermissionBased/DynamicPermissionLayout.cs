// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public class DynamicPermissionLayout : IPermissionsProvider
    {
        private readonly IStorageEngine store;
        private readonly KeyEncoder keyEncoder;

        public static string AclResourceName { get; } = "acl";

        public DynamicPermissionLayout(IStorageEngine store, KeyEncoder keyEncoder)
        {
            this.store = store;
            this.keyEncoder = keyEncoder;
        }

        public async Task<PermissionSet> GetPermissions(IReadOnlyList<SignatureEvidence> identities, LedgerPath path, bool recursiveOnly, string recordName)
        {
            PermissionSet currentPermissions = PermissionSet.Unset;

            Record record = await this.store.GetRecord(new RecordKey(RecordType.Data, path, AclResourceName));

            if (record.Value.Value.Count == 0)
                return PermissionSet.Unset;

            IReadOnlyList<Acl> permissions;
            try
            {
                permissions = Acl.Parse(Encoding.UTF8.GetString(record.Value.ToByteArray()), path, keyEncoder);
            }
            catch (JsonReaderException)
            {
                return PermissionSet.Unset;
            }
            catch (NullReferenceException)
            {
                return PermissionSet.Unset;
            }

            foreach (Acl acl in permissions)
            {
                if (acl.IsMatch(identities, path, recursiveOnly, recordName))
                    currentPermissions = currentPermissions.Add(acl.Permissions);
            }

            return currentPermissions;
        }
    }
}
