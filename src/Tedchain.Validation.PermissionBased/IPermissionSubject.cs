// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using Tedchain.Infrastructure;

namespace Tedchain.Validation.PermissionBased
{
    public interface IPermissionSubject
    {
        bool IsMatch(IReadOnlyList<SignatureEvidence> authentication);
    }
}
