using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Keys;
using Weardian.Server.Domain.Keys.SymmetricKeys;

namespace Weardian.Server.Application.Services.SymmetricKeyServices
{
    public class SymmetricKeyService : ISymmetricKeyService
    {
        public async Task<SymmetricKeyResponseDto> CreateKey(CreateSymmetricKeyRequestDto req)
        {
            if (req.KeyType != KeyType.Encryption &&
                req.KeyType != KeyType.Verification &&
                req.KeyType != KeyType.Signing)

                throw new ArgumentException("Invalid KeyType", nameof(req.KeyType));

            var entity = new SymmetricKey(req.EncryptedKeyBytes)
            {
                Name = req.Name,
                KeyType = req.KeyType,
                WrapAlgorithm = req.Envelope.WrapAlgorithm,
                WrappingKeyId = req.Envelope.WrappingKeyId,
                Tag = req.Envelope.Tag,
                Nonce = req.Envelope.Nonce,
                Ciphertext = req.Envelope.Nonce
            };

            // db call

            return new SymmetricKeyResponseDto(
                entity.PublicId,
                entity.Name,
                entity.KeyType,
                entity.KeyStatus,
                entity.KeyLength,
                entity.CreatedOn
            );

        }

        public async Task<SymmetricKeyResponseDto> GetKeyById(Guid publicId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SymmetricKeyResponseDto>> GetKeys()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveKeyById(Guid publicId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveKeys()
        {
            throw new NotImplementedException();
        }
    }
}
