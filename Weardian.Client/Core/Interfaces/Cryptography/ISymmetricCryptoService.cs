using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces.Cryptography
{
    public interface ISymmetricCryptoService
    {
        public Task<EncryptedEnvelopeDto> CreateEncryptedEnvelopeAsync(string plaintext);
        
    }
}
