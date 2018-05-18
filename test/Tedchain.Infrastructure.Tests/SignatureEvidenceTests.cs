// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using Xunit;

namespace Tedchain.Infrastructure.Tests
{
    public class SignatureEvidenceTests
    {
        private readonly ByteString payload = ByteString.Parse("b27982ff2a4fa0c163da8651ca1902e27b651e68a6d0a4f6bff4a78d7dcf718d");
        private readonly ByteString publicKey = ByteString.Parse("0213b0006543d4ab6e79f49559fbfb18e9d73596d63f39e2f12ebc2c9d51e2eb06");
        private readonly ByteString signature = ByteString.Parse("304402200c7fba6b623efd7e52731a11e6d7b99c2ae752c0f950b7a444ef7fb80162498c02202b01c74a4a04fb120860494de09bd6848f088927a7b07e3c3925b3894c8c89d4");

        [Fact]
        public void VerifySignature_Valid()
        {
            SignatureEvidence evidence = new SignatureEvidence(publicKey, signature);

            Assert.True(evidence.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidPublicKey()
        {
            SignatureEvidence evidence = new SignatureEvidence(
                ByteString.Parse("0013b0006543d4ab6e79f49559fbfb18e9d73596d63f39e2f12ebc2c9d51e2eb06"),
                signature);

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));

            evidence = new SignatureEvidence(
                ByteString.Parse("abcdef12345678"),
                signature);

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidSignature()
        {
            SignatureEvidence evidence = new SignatureEvidence(
                publicKey,
                ByteString.Parse("304402200c7fba6b623efd7e52731a11e6d7b99c2ae752c0f950b7a444ef7fb80162498c02202b01c74a4a04fb120860494de09bd6848f088927a7b07e3c3925b3894c8c89d5"));

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));

            evidence = new SignatureEvidence(
                publicKey,
                ByteString.Parse("3044"));

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));

            evidence = new SignatureEvidence(
                publicKey,
                ByteString.Parse("300680044164616D"));

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));

            evidence = new SignatureEvidence(
                publicKey,
                ByteString.Parse("0304066e5dc0"));

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));
        }

        [Fact]
        public void VerifySignature_InvalidLengths()
        {
            SignatureEvidence evidence = new SignatureEvidence(ByteString.Empty, ByteString.Empty);

            Assert.False(evidence.VerifySignature(payload.ToByteArray()));
        }
    }
}
