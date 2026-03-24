namespace Weardian.Server.Domain.KeyRecords.Symmetric
{
    public class SymmetricKeyRecord : KeyRecordBase
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; } = default!;
        public int KeyLength => Ciphertext.Length * 8;

        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] Tag { get; init; }
        public required byte[] Nonce { get; init; }

        public SymmetricKeyRecord(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length < 16)
                throw new ArgumentException("Ciphertext must be 16 bytes or larger.");

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }

        protected SymmetricKeyRecord() { }
    }
}
