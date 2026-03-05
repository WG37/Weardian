namespace Weardian.Server.Domain.Keys.SymmetricKeys
{
    public class SymmetricKey : KeyBase
    {
        public ReadOnlyMemory<byte> KeyBytes { get; private set; } = default!;
        public int KeyLength => KeyBytes.Length * 8;

        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] Tag { get; init; }
        public required byte[] Nonce { get; init; }
        public required byte[] Ciphertext { get; init; }

        public SymmetricKey(byte[] keyBytes)
        {
            if (keyBytes == null || keyBytes.Length < 16)
                throw new ArgumentException("KeyBytes must be 16 bytes or larger.");

            KeyBytes = new ReadOnlyMemory<byte>((byte[])keyBytes.Clone());
        }

        protected SymmetricKey() { }
    }
}
