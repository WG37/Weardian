namespace Weardian.Client.Domain.KeyRecords.Symmetric
{
    public class SymmetricKeyRecord : KeyRecordBase
    {
        public byte[] WrappedKeyCiphertext { get; private set; }
        public int KeyLength => WrappedKeyCiphertext.Length * 8;

        public Guid EnvelopeId { get; init; }
        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] WrappedKeyTag { get; init; }
        public required byte[] WrappedKeyNonce { get; init; }

        public SymmetricKeyRecord() { }
        public SymmetricKeyRecord(byte[] wrappedKeyCiphertext)
        {
            if (wrappedKeyCiphertext == null || wrappedKeyCiphertext.Length == 0)
                throw new ArgumentException("Ciphertext cannot be null or empty");

            WrappedKeyCiphertext = (byte[])wrappedKeyCiphertext.Clone();
        }
    }
}
