namespace Weardian.Client.Domain.KeyRecords.Symmetric
{
    public class KeyRecord : KeyRecordBase
    {
        public byte[] WrappedKeyCiphertext { get; private set; }

        public Guid EnvelopeId { get; init; }
        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] WrappedKeyTag { get; init; }
        public required byte[] WrappedKeyNonce { get; init; }

        public KeyRecord() { }
        public KeyRecord(byte[] wrappedKeyCiphertext)
        {
            if (wrappedKeyCiphertext == null || wrappedKeyCiphertext.Length == 0)
                throw new ArgumentException("Ciphertext cannot be null or empty");

            WrappedKeyCiphertext = (byte[])wrappedKeyCiphertext.Clone();
        }
    }
}
