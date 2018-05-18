﻿// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Linq;
using System.Security.Cryptography;
using Google.Protobuf;

namespace Tedchain
{
    /// <summary>
    /// Contains utility methods used to serialize and deserialize messages such as
    /// the <see cref="Mutation"/> class and the <see cref="Transaction"/> class.
    /// </summary>
    public static class MessageSerializer
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Serializes a <see cref="Mutation"/> into a byte array.
        /// </summary>
        /// <param name="mutation">The mutation to serialize.</param>
        /// <returns>The serialized mutation.</returns>
        public static byte[] SerializeMutation(Mutation mutation)
        {
            Messages.Mutation mutationBuilder = new Messages.Mutation()
            {
                Namespace = mutation.Namespace.ToProtocolBuffers(),
                Metadata = mutation.Metadata.ToProtocolBuffers()
            };

            mutationBuilder.Records.Add(
                mutation.Records.Select(
                    record =>
                    {
                        var builder = new Messages.Record()
                        {
                            Key = record.Key.ToProtocolBuffers(),
                            Version = record.Version.ToProtocolBuffers()
                        };

                        if (record.Value != null)
                            builder.Value = new Messages.RecordValue() { Data = record.Value.ToProtocolBuffers() };

                        return builder;
                    }));

            return mutationBuilder.ToByteArray();
        }

        /// <summary>
        /// Deserialize a <see cref="Mutation"/> from binary data.
        /// </summary>
        /// <param name="data">The binary data to deserialize.</param>
        /// <returns>The deserialized <see cref="Mutation"/>.</returns>
        public static Mutation DeserializeMutation(ByteString data)
        {
            Messages.Mutation mutation = new Messages.Mutation();
            mutation.MergeFrom(data.ToProtocolBuffers());
            
            return new Mutation(
                new ByteString(mutation.Namespace.ToByteArray()),
                mutation.Records.Select(
                    record => new Record(
                        new ByteString(record.Key.ToByteArray()),
                        record.Value != null ? new ByteString(record.Value.Data.ToByteArray()) : null,
                        new ByteString(record.Version.ToByteArray()))),
                new ByteString(mutation.Metadata.ToByteArray()));
        }

        /// <summary>
        /// Serializes a <see cref="Transaction"/> into a byte array.
        /// </summary>
        /// <param name="transaction">The transaction to serialize.</param>
        /// <returns>The serialized transaction.</returns>
        public static byte[] SerializeTransaction(Transaction transaction)
        {
            Messages.Transaction transactionBuilder = new Messages.Transaction()
            {
                Mutation = transaction.Mutation.ToProtocolBuffers(),
                Timestamp = (long)(transaction.Timestamp - epoch).TotalSeconds,
                TransactionMetadata = transaction.TransactionMetadata.ToProtocolBuffers()
            };

            return transactionBuilder.ToByteArray();
        }

        /// <summary>
        /// Deserialize a <see cref="Transaction"/> from binary data.
        /// </summary>
        /// <param name="data">The binary data to deserialize.</param>
        /// <returns>The deserialized <see cref="Transaction"/>.</returns>
        public static Transaction DeserializeTransaction(ByteString data)
        {
            Messages.Transaction transaction = new Messages.Transaction();
            transaction.MergeFrom(data.ToProtocolBuffers());

            return new Transaction(
                new ByteString(transaction.Mutation.ToByteArray()),
                epoch + TimeSpan.FromSeconds(transaction.Timestamp),
                new ByteString(transaction.TransactionMetadata.ToByteArray()));
        }
        
        /// <summary>
        /// Calculates the hash of an array of bytes.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The result of the hash.</returns>
        public static byte[] ComputeHash(byte[] data)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return hash.ComputeHash(hash.ComputeHash(data));
            }
        }
    }
}
