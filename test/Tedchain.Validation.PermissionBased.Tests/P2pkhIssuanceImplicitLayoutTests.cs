// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class P2pkhIssuanceImplicitLayoutTests
    {
        private static readonly string address = "n12RA1iohYEerfXiBixSoERZG8TP8xQFL2";
        private static readonly SignatureEvidence[] evidence = new[] { new SignatureEvidence(ByteString.Parse("abcdef"), ByteString.Empty) };

        [Fact]
        public async Task GetPermissions_Root()
        {
            P2pkhIssuanceImplicitLayout layout = new P2pkhIssuanceImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/"), true, $"/asset/p2pkh/{address}/");

            Assert.Equal(Access.Unset, result.AccountModify);
            Assert.Equal(Access.Unset, result.AccountCreate);
            Assert.Equal(Access.Permit, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }

        [Fact]
        public async Task GetPermissions_Modify()
        {
            P2pkhIssuanceImplicitLayout layout = new P2pkhIssuanceImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse($"/asset/p2pkh/mgToXgKQqY3asA76uYU82BXMLGrHNm5ZD9/"), true, $"/asset-path/");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }

        [Fact]
        public async Task GetPermissions_Spend()
        {
            P2pkhIssuanceImplicitLayout layout = new P2pkhIssuanceImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse($"/asset/p2pkh/{address}/"), true, $"/asset-path/");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Permit, result.AccountSpend);
            Assert.Equal(Access.Permit, result.DataModify);
        }

        [Fact]
        public async Task GetPermissions_AclRecord()
        {
            P2pkhIssuanceImplicitLayout layout = new P2pkhIssuanceImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse($"/asset/p2pkh/{address}/"), true, $"acl");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }

        [Theory]
        [InlineData("/asset/p2pkh/")]
        [InlineData("/asset/p2pkh/abc/")]
        [InlineData("/asset/p2pkh/mgToXgKQqY3asA76uYU82BXMLGrHNm5ZD9/sub/")]
        [InlineData("/other/")]
        public async Task GetPermissions_NoPermissions(string value)
        {
            P2pkhIssuanceImplicitLayout layout = new P2pkhIssuanceImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse(value), true, $"/asset-path/");

            Assert.Equal(Access.Unset, result.AccountModify);
            Assert.Equal(Access.Unset, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }
    }
}
