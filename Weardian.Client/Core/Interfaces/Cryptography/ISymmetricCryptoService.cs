using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces.Cryptography
{
    internal interface ISymmetricCryptoService
    {
        public Task<EncryptedEnvelopeDto> CreateEncryptedEnvelopeAsync(string plaintext);
        
    }
}
