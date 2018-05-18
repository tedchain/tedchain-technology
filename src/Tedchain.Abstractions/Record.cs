// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;

namespace Tedchain
{
    /// <summary>
    /// Represents a data record identified by a key and a version, and containing a value.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="key">The key of the record.</param>
        /// <param name="value">The value of the record.</param>
        /// <param name="version">The version of the record.</param>
        public Record(ByteString key, ByteString value, ByteString version)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (version == null)
                throw new ArgumentNullException(nameof(version));

            this.Key = key;
            this.Value = value;
            this.Version = version;
        }

        /// <summary>
        /// Gets the key of the record.
        /// </summary>
        public ByteString Key { get; }

        /// <summary>
        /// Gets the value of the record.
        /// </summary>
        public ByteString Value { get; }

        /// <summary>
        /// Gets the version of the record.
        /// </summary>
        public ByteString Version { get; }
    }
}
