// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class PermissionSetTests
    {
        [Fact]
        public void Add_AllCombinations()
        {
            AssertPermissionSet(Access.Permit, PermissionSet.AllowAll.Add(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.AllowAll.Add(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Permit, PermissionSet.AllowAll.Add(PermissionSet.Unset));
            AssertPermissionSet(Access.Deny, PermissionSet.DenyAll.Add(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.DenyAll.Add(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Deny, PermissionSet.DenyAll.Add(PermissionSet.Unset));
            AssertPermissionSet(Access.Permit, PermissionSet.Unset.Add(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.Unset.Add(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Unset, PermissionSet.Unset.Add(PermissionSet.Unset));
        }

        [Fact]
        public void AddLevel_AllCombinations()
        {
            AssertPermissionSet(Access.Permit, PermissionSet.AllowAll.AddLevel(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.AllowAll.AddLevel(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Permit, PermissionSet.AllowAll.AddLevel(PermissionSet.Unset));
            AssertPermissionSet(Access.Permit, PermissionSet.DenyAll.AddLevel(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.DenyAll.AddLevel(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Deny, PermissionSet.DenyAll.AddLevel(PermissionSet.Unset));
            AssertPermissionSet(Access.Permit, PermissionSet.Unset.AddLevel(PermissionSet.AllowAll));
            AssertPermissionSet(Access.Deny, PermissionSet.Unset.AddLevel(PermissionSet.DenyAll));
            AssertPermissionSet(Access.Unset, PermissionSet.Unset.AddLevel(PermissionSet.Unset));
        }

        private void AssertPermissionSet(Access expected, PermissionSet permissions)
        {
            Assert.Equal(expected, permissions.AccountModify);
            Assert.Equal(expected, permissions.AccountNegative);
            Assert.Equal(expected, permissions.AccountSpend);
            Assert.Equal(expected, permissions.AccountCreate);
            Assert.Equal(expected, permissions.DataModify);
        }
    }
}
