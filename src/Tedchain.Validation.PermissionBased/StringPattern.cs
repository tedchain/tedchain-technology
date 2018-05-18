// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain.Validation.PermissionBased
{
    /// <summary>
    /// Represents a pattern for string matching.
    /// </summary>
    public class StringPattern
    {
        /// <summary>
        /// Gets a pattern matching all strings.
        /// </summary>
        public static StringPattern MatchAll { get; } = new StringPattern("", PatternMatchingStrategy.Prefix);

        public StringPattern(string pattern, PatternMatchingStrategy matchingStrategy)
        {
            this.Pattern = pattern;
            this.MatchingStrategy = matchingStrategy;
        }

        /// <summary>
        /// Gets the pattern string.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Gets the matching strategy.
        /// </summary>
        public PatternMatchingStrategy MatchingStrategy { get; }

        /// <summary>
        /// Checks if a string matches the pattern.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>A boolean indicating whether the value matches.</returns>
        public bool IsMatch(string value)
        {
            switch (MatchingStrategy)
            {
                case PatternMatchingStrategy.Exact:
                    return value == Pattern;
                case PatternMatchingStrategy.Prefix:
                    return value.StartsWith(Pattern, StringComparison.Ordinal);
                default:
                    throw new ArgumentOutOfRangeException(nameof(MatchingStrategy));
            }
        }
    }
}
