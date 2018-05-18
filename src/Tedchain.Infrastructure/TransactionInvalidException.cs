// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain.Infrastructure
{
    public class TransactionInvalidException : Exception
    {
        public TransactionInvalidException(string reason)
            : base(string.Format("The transaction was rejected: {0}.", reason))
        {
            this.Reason = reason;
        }

        public string Reason { get; }
    }
}
