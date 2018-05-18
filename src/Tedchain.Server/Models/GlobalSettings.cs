// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

namespace Tedchain.Server.Models
{
    public class GlobalSettings
    {
        public GlobalSettings(ByteString @namespace)
        {
            this.Namespace = @namespace;
        }

        public ByteString Namespace { get; }
    }
}
