// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a digital signature.
    /// </summary>
    public class SignatureEvidence
    {
        public SignatureEvidence(ByteString publicKey, ByteString signature)
        {
            this.PublicKey = publicKey;
            this.Signature = signature;
        }

        /// <summary>
        /// Gets the public key corresponding to the signature.
        /// </summary>
        public ByteString PublicKey { get; }

        /// <summary>
        /// Gets the digital signature.
        /// </summary>
        public ByteString Signature { get; }

        /// <summary>
        /// Verify that the signature is valid.
        /// </summary>
        /// <param name="mutationHash">The data being signed.</param>
        /// <returns>A boolean indicating wheather the signature is valid.</returns>
        public bool VerifySignature(byte[] mutationHash)
        {
            ECKey key;

            try
            {
                key = new ECKey(PublicKey.ToByteArray());
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }

            try
            {
                return key.VerifySignature(mutationHash, Signature.ToByteArray());
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
