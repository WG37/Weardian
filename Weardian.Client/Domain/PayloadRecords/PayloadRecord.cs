namespace Weardian.Client.Domain.PayloadRecords
{
    internal class PayloadRecord
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; }
        public Guid EnvelopeId { get; init; }

        public byte[] Nonce { get; init; }
        public byte[] Tag { get; init; }

        public PayloadRecord(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length < 16)
                throw new ArgumentException("Ciphertext must be at least 16 bytes or larger.");

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }
    }
}
