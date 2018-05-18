// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class StringPatternTests
    {
        [Fact]
        public void IsMatch_Success()
        {
            Assert.True(new StringPattern("name", PatternMatchingStrategy.Exact).IsMatch("name"));
            Assert.False(new StringPattern("name", PatternMatchingStrategy.Exact).IsMatch("name_suffix"));
            Assert.False(new StringPattern("name", PatternMatchingStrategy.Exact).IsMatch("nam"));
            Assert.False(new StringPattern("name", PatternMatchingStrategy.Exact).IsMatch("nams"));

            Assert.True(new StringPattern("name", PatternMatchingStrategy.Prefix).IsMatch("name"));
            Assert.True(new StringPattern("name", PatternMatchingStrategy.Prefix).IsMatch("name_suffix"));
            Assert.False(new StringPattern("name", PatternMatchingStrategy.Prefix).IsMatch("nam"));
            Assert.False(new StringPattern("name", PatternMatchingStrategy.Prefix).IsMatch("nams"));
        }

        [Fact]
        public void IsMatch_InvalidMatchingStrategy()
        {
            StringPattern pattern = new StringPattern("name", (PatternMatchingStrategy)1000);

            Assert.Throws<ArgumentOutOfRangeException>(() => pattern.IsMatch("name"));
        }
    }
}
