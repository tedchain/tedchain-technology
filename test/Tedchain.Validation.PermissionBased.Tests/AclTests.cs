// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class AclTests
    {
        private static readonly P2pkhSubject subject = new P2pkhSubject(new[] { "n12RA1iohYEerfXiBixSoERZG8TP8xQFL2" }, 1, new KeyEncoder(111));
        private static readonly SignatureEvidence[] evidence = new[] { new SignatureEvidence(ByteString.Parse("abcdef"), ByteString.Empty) };
        private static readonly LedgerPath path = LedgerPath.Parse("/root/subitem/");
        private static readonly PermissionSet permissions = PermissionSet.AllowAll;

        [Fact]
        public void IsMatch_Success()
        {
            // Recursive ACL
            Acl acl = new Acl(new[] { subject }, path, true, new StringPattern("name", PatternMatchingStrategy.Exact), permissions);
            // Match (recursiveOnly = true)
            Assert.True(acl.IsMatch(evidence, path, true, "name"));
            // Match (recursiveOnly = false)
            Assert.True(acl.IsMatch(evidence, path, false, "name"));

            // Non-recursive ACL
            acl = new Acl(new[] { subject }, path, false, new StringPattern("name", PatternMatchingStrategy.Exact), permissions);
            // Match (recursiveOnly = false)
            Assert.True(acl.IsMatch(evidence, path, false, "name"));
            // Error: record non recursive (recursiveOnly = true)
            Assert.False(acl.IsMatch(evidence, path, true, "name"));
            // Error: path mismatch
            Assert.False(acl.IsMatch(evidence, LedgerPath.Parse("/"), false, "name"));
            // Error: name mismatch
            Assert.False(acl.IsMatch(evidence, path, false, "n"));
            // Error: identity mismatch
            Assert.False(acl.IsMatch(new[] { new SignatureEvidence(ByteString.Parse("ab"), ByteString.Empty) }, path, false, "name"));
        }

        [Fact]
        public void Parse_Success()
        {
            IReadOnlyList<Acl> result = Acl.Parse(GetValidAcl(), LedgerPath.Parse("/root/path/"), new KeyEncoder(111));

            Assert.Equal(1, result.Count);
            Assert.Equal("/root/path/", result[0].Path.FullPath);
            Assert.Equal("name", result[0].RecordName.Pattern);
            Assert.Equal(PatternMatchingStrategy.Exact, result[0].RecordName.MatchingStrategy);
            Assert.Equal(true, result[0].Recursive);
            Assert.Equal(1, result[0].Subjects.Count);
            Assert.Equal(Access.Permit, result[0].Permissions.AccountModify);
            Assert.Equal(Access.Permit, result[0].Permissions.AccountSpend);
            Assert.Equal(Access.Permit, result[0].Permissions.AccountModify);
            Assert.Equal(Access.Permit, result[0].Permissions.AccountCreate);
            Assert.Equal(Access.Permit, result[0].Permissions.DataModify);
        }

        [Fact]
        public void Parse_Defaults()
        {
            const string acl = @"[{ ""subjects"": [ ], ""permissions"": { } }]";
            IReadOnlyList<Acl> result = Acl.Parse(acl, LedgerPath.Parse("/root/path/"), new KeyEncoder(111));

            Assert.Equal(1, result.Count);
            Assert.Equal("", result[0].RecordName.Pattern);
            Assert.Equal(PatternMatchingStrategy.Prefix, result[0].RecordName.MatchingStrategy);
            Assert.Equal(true, result[0].Recursive);
            Assert.Equal(0, result[0].Subjects.Count);
            Assert.Equal(Access.Unset, result[0].Permissions.AccountModify);
            Assert.Equal(Access.Unset, result[0].Permissions.AccountSpend);
            Assert.Equal(Access.Unset, result[0].Permissions.AccountModify);
            Assert.Equal(Access.Unset, result[0].Permissions.AccountCreate);
            Assert.Equal(Access.Unset, result[0].Permissions.DataModify);
        }

        private static string GetValidAcl()
        {
            return @"
                [{
                    ""subjects"": [ { ""addresses"": [ ""n12RA1iohYEerfXiBixSoERZG8TP8xQFL2"" ], ""required"": 1 } ],
                    ""recursive"": ""true"",
                    ""record_name"": ""name"",
                    ""record_name_matching"": ""Exact"",
                    ""permissions"": {
                        ""account_negative"": ""Permit"",
                        ""account_spend"": ""Permit"",
                        ""account_modify"": ""Permit"",
                        ""account_create"": ""Permit"",
                        ""data_modify"": ""Permit""
                    }
                }]
            ";
        }
    }
}
