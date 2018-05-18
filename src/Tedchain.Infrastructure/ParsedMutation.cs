// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tedchain.Infrastructure
{
    public class ParsedMutation
    {
        public ParsedMutation(
            IList<AccountStatus> accountMutations,
            IList<KeyValuePair<RecordKey, ByteString>> dataRecords)
        {
            this.AccountMutations = new ReadOnlyCollection<AccountStatus>(accountMutations);
            this.DataRecords = new ReadOnlyCollection<KeyValuePair<RecordKey, ByteString>>(dataRecords);
        }

        public IReadOnlyList<AccountStatus> AccountMutations { get; }

        public IReadOnlyList<KeyValuePair<RecordKey, ByteString>> DataRecords { get; }

        public static ParsedMutation Parse(Mutation mutation)
        {
            List<AccountStatus> accountMutations = new List<AccountStatus>();
            List<KeyValuePair<RecordKey, ByteString>> dataRecords = new List<KeyValuePair<RecordKey, ByteString>>();

            foreach (Record record in mutation.Records)
            {
                // This is used for optimistic concurrency and does not participate in the validation
                if (record.Value == null)
                    continue;

                try
                {
                    RecordKey key = RecordKey.Parse(record.Key);
                    switch (key.RecordType)
                    {
                        case RecordType.Account:
                            accountMutations.Add(AccountStatus.FromRecord(key, record));
                            break;
                        case RecordType.Data:
                            dataRecords.Add(new KeyValuePair<RecordKey, ByteString>(key, record.Value));
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "keyData")
                {
                    // Deserializing and re-serializing the record gives a different result
                    throw new TransactionInvalidException("NonCanonicalSerialization");
                }
                catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "path")
                {
                    // The path is invalid
                    throw new TransactionInvalidException("InvalidPath");
                }
                catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "recordType")
                {
                    // The specified record type is unknown
                    throw new TransactionInvalidException("InvalidRecord");
                }
                catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "record")
                {
                    // The value of an ACC record could not be deserialized
                    throw new TransactionInvalidException("InvalidRecord");
                }
            }

            return new ParsedMutation(accountMutations, dataRecords);
        }
    }
}
