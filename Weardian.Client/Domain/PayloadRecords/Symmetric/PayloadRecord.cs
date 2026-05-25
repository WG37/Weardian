using System.Text.Json.Serialization;

namespace Weardian.Client.Domain.PayloadRecords.Symmetric
{
    public class PayloadRecord : PayloadBase
    {
        public byte[] Ciphertext { get; private set; }
        public Guid EnvelopeId { get; init; }

        public int Version { get; init; } = 1;
        public string Algorithm { get; init; }
        public required byte[] Nonce { get; init; }
        public required byte[] Tag { get; init; }

        public PayloadRecord() { }

        [JsonConstructor]
        public PayloadRecord(byte[] ciphertext)
        {
            if (ciphertext == null || ciphertext.Length == 0)
                throw new ArgumentException("Ciphertext cannot be null or empty.");

            Ciphertext = (byte[])ciphertext.Clone();
        }
    }
}
