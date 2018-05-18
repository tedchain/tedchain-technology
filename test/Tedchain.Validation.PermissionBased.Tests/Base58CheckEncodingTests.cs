// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Linq;
using Xunit;

namespace Tedchain.Validation.PermissionBased.Tests
{
    public class Base58CheckEncodingTests
    {
        [Fact]
        public void Encode_Success()
        {
            string data = Base58CheckEncoding.Encode(ByteString.Parse("6f" + "d5188210339b2012cd8f3c5ce3773d49dd7baa4b").Value.ToArray());
            Assert.Equal("mzwhamFUz1oFz2noTGDK9dxq3PAEhNkpuL", data);

            data = Base58CheckEncoding.Encode(ByteString.Parse("6f" + "4641508da141383ce2d1e035c58fad31480bcaac").Value.ToArray());
            Assert.Equal("mmvRqdAcJrSv6M8GozQE4tR3DhfF56c5M1", data);

            data = Base58CheckEncoding.Encode(ByteString.Parse("6f" + "eeab6b6757f135e9ec2f47157c412f80493f1eca").Value.ToArray());
            Assert.Equal("n3GvU9Zo74UZLYgHQcYiLgTFcWfErXFdwe", data);

            data = Base58CheckEncoding.Encode(ByteString.Parse("00" + "691290451961ad74e177bf44f32d9e2fe7454ee6").Value.ToArray());
            Assert.Equal("1AaaBxiLVzo1xZSFpAw3Zm9YBYAYQgQuuU", data);

            data = Base58CheckEncoding.Encode(ByteString.Parse("05" + "36e0ea8e93eaa0285d641305f4c81e563aa570a2").Value.ToArray());
            Assert.Equal("36hBrMeUfevFPZdY2iYSHVaP9jdLd9Np4R", data);
        }

        [Fact]
        public void Decode_Success()
        {
            ByteString data = new ByteString(Base58CheckEncoding.Decode("mzwhamFUz1oFz2noTGDK9dxq3PAEhNkpuL"));
            Assert.Equal(ByteString.Parse("6f" + "d5188210339b2012cd8f3c5ce3773d49dd7baa4b"), data);

            data = new ByteString(Base58CheckEncoding.Decode("mmvRqdAcJrSv6M8GozQE4tR3DhfF56c5M1"));
            Assert.Equal(ByteString.Parse("6f" + "4641508da141383ce2d1e035c58fad31480bcaac"), data);

            data = new ByteString(Base58CheckEncoding.Decode("n3GvU9Zo74UZLYgHQcYiLgTFcWfErXFdwe"));
            Assert.Equal(ByteString.Parse("6f" + "eeab6b6757f135e9ec2f47157c412f80493f1eca"), data);

            data = new ByteString(Base58CheckEncoding.Decode("1AaaBxiLVzo1xZSFpAw3Zm9YBYAYQgQuuU"));
            Assert.Equal(ByteString.Parse("00" + "691290451961ad74e177bf44f32d9e2fe7454ee6"), data);
        }
    }
}
