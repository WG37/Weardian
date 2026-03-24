using Weardian.Client.Core.DTOs.KeyDtos;
using Weardian.Client.Core.Interfaces;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Domain.KeyRecords;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Services
{
    internal sealed class SymmetricKeyManagementService : ISymmetricKeyManagementService
    {
        private readonly ISymmetricCryptoService _symmetricCryptoService;
        public SymmetricKeyManagementService(ISymmetricCryptoService symmetricCryptoService)
        {
            _symmetricCryptoService = symmetricCryptoService;
        }

        public async Task CreateEncryptedPasswordAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));

            if (password.Length < 8)
                throw new ArgumentOutOfRangeException(nameof(password), "Password must have a length of 8 or more characters.");

            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);

            if (!hasUpper || !hasLower || !hasDigit)
                throw new ArgumentException("Password must contain at least one upper, lower and a digit.");

            try
            {
                var envelope = _symmetricCryptoService.CreateEncryptedEnvelope(password);

                var keyRecord = new SymmetricKeyRecord(envelope.WrappedKey.WrappedKeyCiphertext)
                {
                    EnvelopeId = envelope.EnvelopeId,
                    EnvelopeVersion = envelope.WrappedKey.Version,
                    WrapAlgorithm = envelope.WrappedKey.WrapAlgorithm,
                    WrappingKeyId = envelope.WrappedKey.WrappingKeyId,
                    WrappedKeyNonce = envelope.WrappedKey.WrappedKeyNonce,
                    WrappedKeyTag = envelope.WrappedKey.WrappedKeyTag
                };

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to create envelope.", ex);
            }
            
            
            
        }

        public async Task<SymmetricKeyResponseDto> GetKeyByIdAsync(Guid localId)
        {
            throw new NotImplementedException();
        }

        public async Task<SymmetricKeyResponseDto> GetKeysAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveKeyById(Guid localId)
        {
            throw new NotImplementedException();
        }
    }
}
