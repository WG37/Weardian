namespace Weardian.Client.Domain.Keys.Symmetric
{
    internal class SymmetricKey : KeyBase
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; }
        public int KeyLength => Ciphertext.Length * 8;

        public int EnvelopeVersion { get; init; } = 1;
        public required string WrapAlgorithm { get; init; }
        public required Guid WrappingKeyId { get; init; }
        public required byte[] Tag { get; init; }
        public required byte[] Nonce { get; init; }

        public SymmetricKey(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length < 16)
                throw new ArgumentException("Ciphertext must be at least 16 bytes or larger.");

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }
    }
}
