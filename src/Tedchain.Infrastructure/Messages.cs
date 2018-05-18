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
namespace Tedchain.Infrastructure.Messages {

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
            "CgxzY2hlbWEucHJvdG8SCU9wZW5jaGFpbiKXAQoTVHJhbnNhY3Rpb25NZXRh",
            "ZGF0YRJECgpzaWduYXR1cmVzGAEgAygLMjAuT3BlbmNoYWluLlRyYW5zYWN0",
            "aW9uTWV0YWRhdGEuU2lnbmF0dXJlRXZpZGVuY2UaOgoRU2lnbmF0dXJlRXZp",
            "ZGVuY2USEgoKcHVibGljX2tleRgBIAEoDBIRCglzaWduYXR1cmUYAiABKAxi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Infrastructure.Messages.TransactionMetadata), global::Tedchain.Infrastructure.Messages.TransactionMetadata.Parser, new[]{ "Signatures" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence), global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence.Parser, new[]{ "PublicKey", "Signature" }, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  internal sealed partial class TransactionMetadata : pb::IMessage<TransactionMetadata> {
    private static readonly pb::MessageParser<TransactionMetadata> _parser = new pb::MessageParser<TransactionMetadata>(() => new TransactionMetadata());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TransactionMetadata> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tedchain.Infrastructure.Messages.SchemaReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransactionMetadata() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransactionMetadata(TransactionMetadata other) : this() {
      signatures_ = other.signatures_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TransactionMetadata Clone() {
      return new TransactionMetadata(this);
    }

    /// <summary>Field number for the "signatures" field.</summary>
    public const int SignaturesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence> _repeated_signatures_codec
        = pb::FieldCodec.ForMessage(10, global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence.Parser);
    private readonly pbc::RepeatedField<global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence> signatures_ = new pbc::RepeatedField<global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Tedchain.Infrastructure.Messages.TransactionMetadata.Types.SignatureEvidence> Signatures {
      get { return signatures_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TransactionMetadata);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TransactionMetadata other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!signatures_.Equals(other.signatures_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= signatures_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      signatures_.WriteTo(output, _repeated_signatures_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += signatures_.CalculateSize(_repeated_signatures_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TransactionMetadata other) {
      if (other == null) {
        return;
      }
      signatures_.Add(other.signatures_);
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
            signatures_.AddEntriesFrom(input, _repeated_signatures_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the TransactionMetadata message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    internal static partial class Types {
      internal sealed partial class SignatureEvidence : pb::IMessage<SignatureEvidence> {
        private static readonly pb::MessageParser<SignatureEvidence> _parser = new pb::MessageParser<SignatureEvidence>(() => new SignatureEvidence());
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<SignatureEvidence> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Tedchain.Infrastructure.Messages.TransactionMetadata.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public SignatureEvidence() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public SignatureEvidence(SignatureEvidence other) : this() {
          publicKey_ = other.publicKey_;
          signature_ = other.signature_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public SignatureEvidence Clone() {
          return new SignatureEvidence(this);
        }

        /// <summary>Field number for the "public_key" field.</summary>
        public const int PublicKeyFieldNumber = 1;
        private pb::ByteString publicKey_ = pb::ByteString.Empty;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString PublicKey {
          get { return publicKey_; }
          set {
            publicKey_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "signature" field.</summary>
        public const int SignatureFieldNumber = 2;
        private pb::ByteString signature_ = pb::ByteString.Empty;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString Signature {
          get { return signature_; }
          set {
            signature_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as SignatureEvidence);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(SignatureEvidence other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (PublicKey != other.PublicKey) return false;
          if (Signature != other.Signature) return false;
          return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (PublicKey.Length != 0) hash ^= PublicKey.GetHashCode();
          if (Signature.Length != 0) hash ^= Signature.GetHashCode();
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (PublicKey.Length != 0) {
            output.WriteRawTag(10);
            output.WriteBytes(PublicKey);
          }
          if (Signature.Length != 0) {
            output.WriteRawTag(18);
            output.WriteBytes(Signature);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (PublicKey.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeBytesSize(PublicKey);
          }
          if (Signature.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeBytesSize(Signature);
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(SignatureEvidence other) {
          if (other == null) {
            return;
          }
          if (other.PublicKey.Length != 0) {
            PublicKey = other.PublicKey;
          }
          if (other.Signature.Length != 0) {
            Signature = other.Signature;
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
                PublicKey = input.ReadBytes();
                break;
              }
              case 18: {
                Signature = input.ReadBytes();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
