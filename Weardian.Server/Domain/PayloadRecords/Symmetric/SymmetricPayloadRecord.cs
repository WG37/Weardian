namespace Weardian.Server.Domain.PayloadRecords.Symmetric
{
    public class SymmetricPayloadRecord : PayloadBase
    {
        public ReadOnlyMemory<byte> Ciphertext { get; private set; }
        public Guid EnvelopeId { get; init; }

        public int EnvelopeVersion { get; init; } = 1;
        public required string Algorithm { get; init; }
        public required byte[] Nonce { get; init; } 
        public required byte[] Tag { get; init; }

        public SymmetricPayloadRecord(byte[] ciphertext)
        {
            if (ciphertext == null)
                throw new ArgumentException("Ciphertext is null", nameof(ciphertext));

            Ciphertext = new ReadOnlyMemory<byte>((byte[])ciphertext.Clone());
        }

        protected SymmetricPayloadRecord() { }
    }
}
