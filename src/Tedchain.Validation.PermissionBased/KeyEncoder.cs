// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using System.Security.Cryptography;

namespace Tedchain.Validation.PermissionBased
{
    public class KeyEncoder
    {
        private readonly byte versionByte;

        public KeyEncoder(byte versionByte)
        {
            this.versionByte = versionByte;
        }

        public bool IsP2pkh(string address)
        {
            try
            {
                byte[] pubKeyHash = Base58CheckEncoding.Decode(address);
                if (pubKeyHash.Length != 21 || pubKeyHash[0] != versionByte)
                    return false;
                else
                    return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public string GetPubKeyHash(ByteString pubKey)
        {
            Org.BouncyCastle.Crypto.Digests.RipeMD160Digest ripe = new Org.BouncyCastle.Crypto.Digests.RipeMD160Digest();

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] shaResult = sha256.ComputeHash(pubKey.ToByteArray());
                ripe.BlockUpdate(shaResult, 0, shaResult.Length);
                byte[] hash = new byte[20];
                ripe.DoFinal(hash, 0);
                return Base58CheckEncoding.Encode(new byte[] { versionByte }.Concat(hash).ToArray());
            }
        }
    }
}
