// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tedchain
{
    /// <summary>
    /// Represent a mutation performed on a set of data records.
    /// </summary>
    public class Mutation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mutation"/> class.
        /// </summary>
        /// <param name="@namespace">The namespace in which the mutation operates.</param>
        /// <param name="records">A collection of all the records affected by the mutation.</param>
        /// <param name="metadata">The metadata associated with the mutation.</param>
        public Mutation(ByteString @namespace, IEnumerable<Record> records, ByteString metadata)
        {
            if (@namespace == null)
                throw new ArgumentNullException(nameof(@namespace));

            if (records == null)
                throw new ArgumentNullException(nameof(records));

            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            this.Namespace = @namespace;
            this.Records = records.ToList().AsReadOnly();
            this.Metadata = metadata;

            // Records must not be null
            if (this.Records.Any(entry => entry == null))
                throw new ArgumentNullException(nameof(records));

            // There must not be any duplicate keys
            HashSet<ByteString> keys = new HashSet<ByteString>();
            foreach (Record record in this.Records)
            {
                if (keys.Contains(record.Key))
                    throw new ArgumentNullException(nameof(records));

                keys.Add(record.Key);
            }
        }

        /// <summary>
        /// Gets the namespace in which the mutation operates.
        /// </summary>
        public ByteString Namespace { get; }

        /// <summary>
        /// Gets a collection of all the records affected by the mutation.
        /// </summary>
        public IReadOnlyList<Record> Records { get; }

        /// <summary>
        /// Gets the metadata associated with the mutation.
        /// </summary>
        public ByteString Metadata { get; }
    }
}
