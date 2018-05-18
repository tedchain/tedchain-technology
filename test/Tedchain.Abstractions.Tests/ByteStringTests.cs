// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.IO;
using Xunit;

namespace Tedchain.Tests
{
    public class ByteStringTests
    {
        [Fact]
        public void Constructor_Success()
        {
            byte[] sourceArray = new byte[] { 18, 178, 255, 70, 0 };
            ByteString result = new ByteString(sourceArray);
            sourceArray[4] = 1;

            Assert.NotSame(sourceArray, result.Value);
            Assert.Equal<byte>(new byte[] { 18, 178, 255, 70, 0 }, result.Value);
        }

        [Fact]
        public void Parse_Success()
        {
            ByteString result = ByteString.Parse("12b2FE460035789ACd");

            Assert.Equal<byte>(new byte[] { 18, 178, 254, 70, 0, 53, 120, 154, 205 }, result.Value);
        }

        [Fact]
        public void Parse_InvalidLength()
        {
            Assert.Throws<FormatException>(
                () => ByteString.Parse("12b2ff460"));
        }

        [Fact]
        public void Parse_InvalidCharacter()
        {
            Assert.Throws<FormatException>(
                () => ByteString.Parse("1G"));

            Assert.Throws<FormatException>(
                () => ByteString.Parse("1/"));
        }

        [Fact]
        public void Parse_Null()
        {
            Assert.Throws<FormatException>(
                () => ByteString.Parse(null));
        }

        [Fact]
        public void ToArray_Success()
        {
            byte[] sourceArray = new byte[] { 18, 178, 255, 70, 0 };
            ByteString result = new ByteString(sourceArray);

            Assert.Equal<byte>(new byte[] { 18, 178, 255, 70, 0 }, result.ToByteArray());
        }

        [Fact]
        public void ToString_Success()
        {
            string result = new ByteString(new byte[] { 18, 178, 255, 70, 0 }).ToString();

            Assert.Equal("12b2ff4600", result);
        }

        [Fact]
        public void ToStream_Success()
        {
            ByteString data = ByteString.Parse("abcdef");

            byte[] result = new BinaryReader(data.ToStream()).ReadBytes(3);

            Assert.Equal(3, data.ToStream().Length);
            Assert.Equal(data, new ByteString(result));
            Assert.Equal(false, data.ToStream().CanWrite);
        }

        [Fact]
        public void ToStream_Immutable()
        {
            ByteString data = ByteString.Parse("abcdef");

            Assert.Throws<NotSupportedException>(() => data.ToStream().WriteByte(1));
        }

        [Fact]
        public void Equals_Success()
        {
            Assert.True(ByteString.Parse("abcd").Equals(ByteString.Parse("abcd")));
            Assert.False(ByteString.Parse("abcd").Equals(ByteString.Parse("abce")));
            Assert.False(ByteString.Parse("abcd").Equals(ByteString.Parse("abcdef")));
            Assert.False(ByteString.Parse("abcdef").Equals(ByteString.Parse("abcd")));
            Assert.False(ByteString.Parse("abcd").Equals(null));

            Assert.True(ByteString.Parse("abcd").Equals((object)ByteString.Parse("abcd")));
            Assert.False(ByteString.Parse("abcd").Equals(4));
        }
    }
}
