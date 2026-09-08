namespace Weardian.Server.Domain.KeyRecords.Symmetric
{
    public class SymmetricKeyRecord : KeyRecordBase
    {
        public ReadOnlyMemory<byte> WrappedKeyCiphertext { get; private set; } 

        public Guid EnvelopeId { get; init; }
        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] WrappedKeyTag { get; init; }
        public required byte[] WrappedKeyNonce { get; init; }

        public SymmetricKeyRecord(byte[] wrappedKeyCiphertext)
        {
            if (wrappedKeyCiphertext == null)
                throw new ArgumentException("Ciphertext cannot be empty", nameof(wrappedKeyCiphertext));

            WrappedKeyCiphertext = new ReadOnlyMemory<byte>((byte[])wrappedKeyCiphertext.Clone());
        }

        protected SymmetricKeyRecord() { }
    }
}
