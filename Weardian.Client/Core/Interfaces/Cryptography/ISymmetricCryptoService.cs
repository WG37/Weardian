using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces.Cryptography
{
    public interface ISymmetricCryptoService
    {
        public Task<EncryptedEnvelopeDto> CreateEncryptedEnvelopeAsync(string plaintext);
        public Task<string> DecryptEncryptedEnvelopeAsync(KeyRecord keyRecord, PayloadRecord payloadRecord);
    }
}
