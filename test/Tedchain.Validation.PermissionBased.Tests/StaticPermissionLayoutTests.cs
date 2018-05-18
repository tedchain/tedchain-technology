// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class StaticPermissionLayoutTests
    {
        private static readonly P2pkhSubject subject = new P2pkhSubject(new[] { "n12RA1iohYEerfXiBixSoERZG8TP8xQFL2" }, 1, new KeyEncoder(111));
        private static readonly SignatureEvidence[] evidence = new[] { new SignatureEvidence(ByteString.Parse("abcdef"), ByteString.Empty) };
        private static readonly LedgerPath path = LedgerPath.Parse("/root/subitem/");
        private static readonly PermissionSet permissions = PermissionSet.AllowAll;

        [Fact]
        public async Task GetPermissions_Match()
        {
            StaticPermissionLayout layout = new StaticPermissionLayout(new[]
            {
                new Acl(new[] { subject }, path, true, new StringPattern("name", PatternMatchingStrategy.Exact), permissions)
            });

            PermissionSet result = await layout.GetPermissions(evidence, path, true, "name");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountNegative);
            Assert.Equal(Access.Permit, result.AccountSpend);
            Assert.Equal(Access.Permit, result.DataModify);
        }

        [Fact]
        public async Task GetPermissions_NoMatch()
        {
            StaticPermissionLayout layout = new StaticPermissionLayout(new[]
            {
                new Acl(new[] { subject }, path, true, new StringPattern("name", PatternMatchingStrategy.Exact), permissions)
            });

            PermissionSet result = await layout.GetPermissions(evidence, path, true, "other_name");

            Assert.Equal(Access.Unset, result.AccountModify);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }
    }
}
