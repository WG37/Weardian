using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces.Cryptography
{
    internal interface ISymmetricCryptoService
    {
        public EncryptedEnvelopeDto CreateEncryptedEnvelope(string plaintext);
        
    }
}
