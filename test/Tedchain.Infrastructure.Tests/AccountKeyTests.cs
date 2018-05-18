// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using Xunit;

namespace Tedchain.Infrastructure.Tests
{
    public class AccountKeyTests
    {
        [Fact]
        public void AccountKey_Success()
        {
            AccountKey result = new AccountKey(LedgerPath.Parse("/path/"), LedgerPath.Parse("/asset/"));

            Assert.Equal("/path/", result.Account.FullPath);
            Assert.Equal("/asset/", result.Asset.FullPath);
        }

        [Fact]
        public void AccountKey_ArgumentNullException()
        {
            ArgumentNullException exception;

            exception = Assert.Throws<ArgumentNullException>(() => new AccountKey(null, LedgerPath.Parse("/asset/")));
            Assert.Equal("account", exception.ParamName);

            exception = Assert.Throws<ArgumentNullException>(() => new AccountKey(LedgerPath.Parse("/path/"), null));
            Assert.Equal("asset", exception.ParamName);
        }

        [Fact]
        public void Equals_Success()
        {
            Assert.True(AccountKey.Parse("/abc/", "/def/").Equals(AccountKey.Parse("/abc/", "/def/")));
            Assert.False(AccountKey.Parse("/abc/", "/def/").Equals(AccountKey.Parse("/abc/", "/ghi/")));
            Assert.False(AccountKey.Parse("/abc/", "/def/").Equals(null));
            Assert.False(AccountKey.Parse("/abc/", "/def/").Equals(100));
        }

        [Fact]
        public void Equals_Object()
        {
            Assert.True(AccountKey.Parse("/abc/", "/def/").Equals((object)AccountKey.Parse("/abc/", "/def/")));
        }

        [Fact]
        public void GetHashCode_Success()
        {
            Assert.Equal(AccountKey.Parse("/abc/", "/def/").GetHashCode(), AccountKey.Parse("/abc/", "/def/").GetHashCode());
        }
    }
}
