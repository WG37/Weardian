namespace Weardian.Client.Domain.PayloadRecords
{
    public class PayloadRecord : PayloadBase
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; }
        public Guid EnvelopeId { get; init; }

        public int Version { get; init; } = 1;
        public string Algorithm { get; init; }
        public required byte[] Nonce { get; init; }
        public required byte[] Tag { get; init; }

        public PayloadRecord(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length == 0)
                throw new ArgumentException("Ciphertext cannot be null or empty.");

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }
    }
}
