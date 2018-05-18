// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tedchain.Infrastructure
{
    /// <summary>
    /// Represents a service capable of verifying a mutation.
    /// </summary>
    public interface IMutationValidator
    {
        /// <summary>
        /// Validates a mutation.
        /// </summary>
        /// <param name="mutation">The mutation to validate.</param>
        /// <param name="authentication">Authentication data provided along with the mutation.</param>
        /// <param name="accounts">Dictionary containing the current version of records being affected.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IList<Mutation>> Validate(ParsedMutation mutation, IReadOnlyList<SignatureEvidence> authentication, IReadOnlyDictionary<AccountKey, AccountStatus> accounts);
    }
}
