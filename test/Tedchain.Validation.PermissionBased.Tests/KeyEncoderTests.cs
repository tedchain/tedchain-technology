// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class KeyEncoderTests
    {
        [Fact]
        public void IsP2pkh_Success()
        {
            KeyEncoder keyEncoder = new KeyEncoder(111);

            // Valid
            Assert.Equal(true, keyEncoder.IsP2pkh("mfiCwNxuFYMtb5ytCacgzDAineD2GNCnYo"));
            // Wrong checksum
            Assert.Equal(false, keyEncoder.IsP2pkh("mfiCwNxuFYMtb5ytCacgzDAineD2GNCnYp"));
            // Invalid size
            Assert.Equal(false, keyEncoder.IsP2pkh("2Qx7aDxjSh772"));
            // Empty
            Assert.Equal(false, keyEncoder.IsP2pkh(""));
            // Invalid version byte
            Assert.Equal(false, keyEncoder.IsP2pkh("1CCW1yPxC8meB7JzF8xEwaad4DxksFqhrQ"));
        }

        [Fact]
        public void GetPubKeyHash_Success()
        {
            KeyEncoder keyEncoder = new KeyEncoder(111);

            Assert.Equal("n12RA1iohYEerfXiBixSoERZG8TP8xQFL2", keyEncoder.GetPubKeyHash(ByteString.Parse("abcdef")));
        }
    }
}
