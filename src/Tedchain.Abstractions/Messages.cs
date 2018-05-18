// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Tedchain.Messages {

  /// <summary>Holder for reflection information generated from schema.proto</summary>
  internal static partial class SchemaReflection {

    #region Descriptor
    /// <summary>File descriptor for schema.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SchemaReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxzY2hlbWEucHJvdG8SCU9wZW5jaGFpbiIbCgtSZWNvcmRWYWx1ZRIMCgRk",
            "YXRhGAEgASgMIk0KBlJlY29yZBILCgNrZXkYASABKAwSJQoFdmFsdWUYAiAB",
            "KAsyFi5PcGVuY2hhaW4uUmVjb3JkVmFsdWUSDwoHdmVyc2lvbhgDIAEoDCJT",
            "CghNdXRhdGlvbhIRCgluYW1lc3BhY2UYASABKAwSIgoHcmVjb3JkcxgCIAMo",
            "CzIRLk9wZW5jaGFpbi5SZWNvcmQSEAoIbWV0YWRhdGEYAyABKAwiUAoLVHJh",
            "bnNhY3Rpb24SEAoIbXV0YXRpb24YASABKAwSEQoJdGltZXN0YW1wGAIgASgD",
            "EhwKFHRyYW5zYWN0aW9uX21ldGFkYXRhGAMgASgMYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Messages.RecordValue), global::Tedchain.Messages.RecordValue.Parser, new[]{ "Data" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Messages.Record), global::Tedchain.Messages.Record.Parser, new[]{ "Key", "Value", "Version" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Messages.Mutation), global::Tedchain.Messages.Mutation.Parser, new[]{ "Namespace", "Records", "Metadata" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Messages.Transaction), global::Tedchain.Messages.Transaction.Parser, new[]{ "Mutation", "Timestamp", "TransactionMetadata" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  internal sealed partial class RecordValue : pb::IMessage<RecordValue> {
    private static readonly pb::MessageParser<RecordValue> _parser = new pb::MessageParser<RecordValue>(() => new RecordValue());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RecordValue> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tedchain.Messages.SchemaReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecordValue() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecordValue(RecordValue other) : this() {
      data_ = other.data_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RecordValue Clone() {
      return new RecordValue(this);
    }

    /// <summary>Field number for the "data" field.</summary>
    public const int DataFieldNumber = 1;
    private pb::ByteString data_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Data {
      get { return data_; }
      set {
        data_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RecordValue);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RecordValue other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Data != other.Data) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Data.Length != 0) hash ^= Data.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Data.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Data);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Data.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Data);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RecordValue other) {
      if (other == null) {
        return;
      }
      if (other.Data.Length != 0) {
        Data = other.Data;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Data = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  internal sealed partial class Record : pb::IMessage<Record> {
    private static readonly pb::MessageParser<Record> _parser = new pb::MessageParser<Record>(() => new Record());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Record> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tedchain.Messages.SchemaReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Record() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Record(Record other) : this() {
      key_ = other.key_;
      Value = other.value_ != null ? other.Value.Clone() : null;
      version_ = other.version_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Record Clone() {
      return new Record(this);
    }

    /// <summary>Field number for the "key" field.</summary>
    public const int KeyFieldNumber = 1;
    private pb::ByteString key_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Key {
      get { return key_; }
      set {
        key_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 2;
    private global::Tedchain.Messages.RecordValue value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tedchain.Messages.RecordValue Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    /// <summary>Field number for the "version" field.</summary>
    public const int VersionFieldNumber = 3;
    private pb::ByteString version_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Version {
      get { return version_; }
      set {
        version_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Record);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Record other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Key != other.Key) return false;
      if (!object.Equals(Value, other.Value)) return false;
      if (Version != other.Version) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Key.Length != 0) hash ^= Key.GetHashCode();
      if (value_ != null) hash ^= Value.GetHashCode();
      if (Version.Length != 0) hash ^= Version.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Key.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Key);
      }
      if (value_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Value);
      }
      if (Version.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(Version);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Key.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Key);
      }
      if (value_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Value);
      }
      if (Version.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Version);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Record other) {
      if (other == null) {
        return;
      }
      if (other.Key.Length != 0) {
        Key = other.Key;
      }
      if (other.value_ != null) {
        if (value_ == null) {
          value_ = new global::Tedchain.Messages.RecordValue();
        }
        Value.MergeFrom(other.Value);
      }
      if (other.Version.Length != 0) {
        Version = other.Version;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Key = input.ReadBytes();
            break;
          }
          case 18: {
            if (value_ == null) {
              value_ = new global::Tedchain.Messages.RecordValue();
            }
            input.ReadMessage(value_);
            break;
          }
          case 26: {
            Version = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  internal sealed partial class Mutation : pb::IMessage<Mutation> {
    private static readonly pb::MessageParser<Mutation> _parser = new pb::MessageParser<Mutation>(() => new Mutation());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Mutation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tedchain.Messages.SchemaReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mutation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mutation(Mutation other) : this() {
      namespace_ = other.namespace_;
      records_ = other.records_.Clone();
      metadata_ = other.metadata_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mutation Clone() {
      return new Mutation(this);
    }

    /// <summary>Field number for the "namespace" field.</summary>
    public const int NamespaceFieldNumber = 1;
    private pb::ByteString namespace_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Namespace {
      get { return namespace_; }
      set {
        namespace_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "records" field.</summary>
    public const int RecordsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Tedchain.Messages.Record> _repeated_records_codec
        = pb::FieldCodec.ForMessage(18, global::Tedchain.Messages.Record.Parser);
    private readonly pbc::RepeatedField<global::Tedchain.Messages.Record> records_ = new pbc::RepeatedField<global::Tedchain.Messages.Record>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tedchain.Messages.Record> Records {
      get { return records_; }
    }

    /// <summary>Field number for the "metadata" field.</summary>
    public const int MetadataFieldNumber = 3;
    private pb::ByteString metadata_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Metadata {
      get { return metadata_; }
      set {
        metadata_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Mutation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Mutation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Namespace != other.Namespace) return false;
      if(!records_.Equals(other.records_)) return false;
      if (Metadata != other.Metadata) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Namespace.Length != 0) hash ^= Namespace.GetHashCode();
      hash ^= records_.GetHashCode();
      if (Metadata.Length != 0) hash ^= Metadata.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Namespace.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Namespace);
      }
      records_.WriteTo(output, _repeated_records_codec);
      if (Metadata.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(Metadata);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Namespace.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Namespace);
      }
      size += records_.CalculateSize(_repeated_records_codec);
      if (Metadata.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Metadata);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Mutation other) {
      if (other == null) {
        return;
      }
      if (other.Namespace.Length != 0) {
        Namespace = other.Namespace;
      }
      records_.Add(other.records_);
      if (other.Metadata.Length != 0) {
        Metadata = other.Metadata;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Namespace = input.ReadBytes();
            break;
          }
          case 18: {
            records_.AddEntriesFrom(input, _repeated_records_codec);
            break;
          }
          case 26: {
            Metadata = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  internal sealed partial class Transaction : pb::IMessage<Transaction> {
    private static readonly pb::MessageParser<Transaction> _parser = new pb::MessageParser<Transaction>(() => new Transaction());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Transaction> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tedchain.Messages.SchemaReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction(Transaction other) : this() {
      mutation_ = other.mutation_;
      timestamp_ = other.timestamp_;
      transactionMetadata_ = other.transactionMetadata_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Transaction Clone() {
      return new Transaction(this);
    }

    /// <summary>Field number for the "mutation" field.</summary>
    public const int MutationFieldNumber = 1;
    private pb::ByteString mutation_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Mutation {
      get { return mutation_; }
      set {
        mutation_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 2;
    private long timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "transaction_metadata" field.</summary>
    public const int TransactionMetadataFieldNumber = 3;
    private pb::ByteString transactionMetadata_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString TransactionMetadata {
      get { return transactionMetadata_; }
      set {
        transactionMetadata_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Transaction);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Transaction other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Mutation != other.Mutation) return false;
      if (Timestamp != other.Timestamp) return false;
      if (TransactionMetadata != other.TransactionMetadata) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Mutation.Length != 0) hash ^= Mutation.GetHashCode();
      if (Timestamp != 0L) hash ^= Timestamp.GetHashCode();
      if (TransactionMetadata.Length != 0) hash ^= TransactionMetadata.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Mutation.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Mutation);
      }
      if (Timestamp != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(Timestamp);
      }
      if (TransactionMetadata.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(TransactionMetadata);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Mutation.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Mutation);
      }
      if (Timestamp != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Timestamp);
      }
      if (TransactionMetadata.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(TransactionMetadata);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Transaction other) {
      if (other == null) {
        return;
      }
      if (other.Mutation.Length != 0) {
        Mutation = other.Mutation;
      }
      if (other.Timestamp != 0L) {
        Timestamp = other.Timestamp;
      }
      if (other.TransactionMetadata.Length != 0) {
        TransactionMetadata = other.TransactionMetadata;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Mutation = input.ReadBytes();
            break;
          }
          case 16: {
            Timestamp = input.ReadInt64();
            break;
          }
          case 26: {
            TransactionMetadata = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
