// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tedchain.Infrastructure;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class DynamicPermissionLayoutTests
    {
        private static readonly SignatureEvidence[] evidence = new[] { new SignatureEvidence(ByteString.Parse("abcdef"), ByteString.Empty) };

        [Fact]
        public async Task GetPermissions_Match()
        {
            DynamicPermissionLayout layout = new DynamicPermissionLayout(new TestStore(), new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/root/subitem/"), true, "name");

            AssertPermissionSet(Access.Permit, result);
        }

        [Fact]
        public async Task GetPermissions_NoMatch()
        {
            DynamicPermissionLayout layout = new DynamicPermissionLayout(new TestStore(), new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/root/subitem/"), true, "other");

            AssertPermissionSet(Access.Unset, result);
        }

        [Fact]
        public async Task GetPermissions_NoAcl()
        {
            DynamicPermissionLayout layout = new DynamicPermissionLayout(new TestStore(), new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/root/other/"), true, "name");

            AssertPermissionSet(Access.Unset, result);
        }

        [Fact]
        public async Task GetPermissions_InvalidAcl()
        {
            DynamicPermissionLayout layout = new DynamicPermissionLayout(new TestStore(), new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/root/invalid/"), true, "name");

            AssertPermissionSet(Access.Unset, result);
        }

        [Fact]
        public async Task GetPermissions_JsonComments()
        {
            DynamicPermissionLayout layout = new DynamicPermissionLayout(new TestStore(), new KeyEncoder(111));

            PermissionSet result = await layout.GetPermissions(evidence, LedgerPath.Parse("/root/comment/"), true, "name");

            AssertPermissionSet(Access.Unset, result);
        }

        private void AssertPermissionSet(Access expected, PermissionSet permissions)
        {
            Assert.Equal(expected, permissions.AccountModify);
            Assert.Equal(expected, permissions.AccountNegative);
            Assert.Equal(expected, permissions.AccountSpend);
            Assert.Equal(expected, permissions.AccountCreate);
            Assert.Equal(expected, permissions.DataModify);
        }

        private class TestStore : IStorageEngine
        {
            public Task Initialize()
            {
                throw new NotImplementedException();
            }

            public Task AddTransactions(IEnumerable<ByteString> transactions)
            {
                throw new NotImplementedException();
            }

            public Task<ByteString> GetLastTransaction()
            {
                throw new NotImplementedException();
            }

            public Task<IReadOnlyList<Record>> GetRecords(IEnumerable<ByteString> keys)
            {
                return Task.FromResult<IReadOnlyList<Record>>(keys.Select(key =>
                {
                    RecordKey recordKey = RecordKey.Parse(key);

                    if (recordKey.Name == "acl")
                    {
                        if (recordKey.Path.FullPath == "/root/subitem/")
                        {
                            return new Record(key, GetValidAcl(), ByteString.Empty);
                        }
                        else if (recordKey.Path.FullPath == "/root/invalid/")
                        {
                            return new Record(key, GetInvalidAcl(), ByteString.Empty);
                        }
                        else if (recordKey.Path.FullPath == "/root/comment/")
                        {
                            return new Record(key, GetCommentedAcl(), ByteString.Empty);
                        }
                    }

                    return new Record(key, ByteString.Empty, ByteString.Empty);
                })
                .ToList());
            }

            private static ByteString GetValidAcl()
            {
                return new ByteString(
                    Encoding.UTF8.GetBytes(@"
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
                    "));
            }

            private static ByteString GetInvalidAcl()
            {
                return new ByteString(
                    Encoding.UTF8.GetBytes(@"[{ ""invalid"" }]"));
            }

            private static ByteString GetCommentedAcl()
            {
                return new ByteString(
                    Encoding.UTF8.GetBytes(@"[ /* Comment */ { }]"));
            }

            public Task<IReadOnlyList<ByteString>> GetTransactions(ByteString from)
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            { }
        }
    }
}
