// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Threading.Tasks;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class P2pkhImplicitLayoutTests
    {
        private static readonly string address = "n12RA1iohYEerfXiBixSoERZG8TP8xQFL2";
        private static readonly SignatureEvidence[] evidence = new[] { new SignatureEvidence(ByteString.Parse("abcdef"), ByteString.Empty) };

        [Fact]
        public async Task GetPermissions_Spend()
        {
            P2pkhImplicitLayout layout = new P2pkhImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse($"/p2pkh/{address}/"), true, $"/asset-path/");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Permit, result.AccountSpend);
            Assert.Equal(Access.Permit, result.DataModify);
        }

        [Fact]
        public async Task GetPermissions_Modify()
        {
            P2pkhImplicitLayout layout = new P2pkhImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse($"/p2pkh/mgToXgKQqY3asA76uYU82BXMLGrHNm5ZD9/"), true, $"/asset-path/");

            Assert.Equal(Access.Permit, result.AccountModify);
            Assert.Equal(Access.Permit, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }

        [Theory]
        [InlineData("/asset/p2pkh/")]
        [InlineData("/asset/p2pkh/abc/")]
        [InlineData("/p2pkh/mgToXgKQqY3asA76uYU82BXMLGrHNm5ZD9/sub/")]
        [InlineData("/other/")]
        public async Task GetPermissions_NoPermissions(string value)
        {
            P2pkhImplicitLayout layout = new P2pkhImplicitLayout(new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse(value), true, $"/asset-path/");

            Assert.Equal(Access.Unset, result.AccountModify);
            Assert.Equal(Access.Unset, result.AccountCreate);
            Assert.Equal(Access.Unset, result.AccountNegative);
            Assert.Equal(Access.Unset, result.AccountSpend);
            Assert.Equal(Access.Unset, result.DataModify);
        }
    }
}
