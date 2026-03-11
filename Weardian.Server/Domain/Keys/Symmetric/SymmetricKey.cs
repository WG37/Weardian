namespace Weardian.Server.Domain.Keys.Symmetric
{
    public class SymmetricKey : KeyBase
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; } = default!;
        public int KeyLength => Ciphertext.Length * 8;

        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] Tag { get; init; }
        public required byte[] Nonce { get; init; }

        public SymmetricKey(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length < 16)
                throw new ArgumentException("Ciphertext must be 16 bytes or larger.");

            if (ciphertext.Length != 16 &&
                ciphertext.Length != 24 &&
                ciphertext.Length != 32)
                throw new ArgumentException("Ciphertext must be: 16, 24, 32 bytes or 128, 192, 256 bits.");

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }

        protected SymmetricKey() { }
    }
}
