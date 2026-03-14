using Weardian.Server.Application.DTOs.CryptographyDto;
using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.Keys;
using Weardian.Server.Domain.Keys.Symmetric;

namespace Weardian.Server.Application.Services.SymmetricKeyServices
{
    public class SymmetricKeyService : ISymmetricKeyService
    {
        private readonly ISymmetricKeyRepository _keyRepository;

        public SymmetricKeyService(ISymmetricKeyRepository keyRepository)
        {
            _keyRepository = keyRepository;
        }

        public async Task<SymmetricKeyResponseDto> CreateKey(CreateSymmetricKeyRequestDto req, string userId)
        {
            if (req.KeyType != KeyType.Encryption &&
                req.KeyType != KeyType.Verification &&
                req.KeyType != KeyType.Signing)

                throw new ArgumentException("Invalid KeyType", nameof(req.KeyType));

            var key = new SymmetricKey(req.Envelope.Ciphertext)
            {
                Name = req.Name,
                KeyType = req.KeyType,
                WrapAlgorithm = req.Envelope.WrapAlgorithm,
                WrappingKeyId = req.Envelope.WrappingKeyId,
                Tag = req.Envelope.Tag,
                Nonce = req.Envelope.Nonce,
                UserId = userId
            };

            await _keyRepository.AddAsync(key);

            return new SymmetricKeyResponseDto(
                key.PublicId,
                key.Name,
                key.KeyType,
                key.KeyStatus,
                key.KeyLength,
                new EncryptedEvelopeResponseDto(
                    key.EnvelopeVersion,
                    key.WrapAlgorithm,
                    key.WrappingKeyId,
                    key.Ciphertext.ToArray(),
                    key.Tag,
                    key.Nonce
            ),
                key.CreatedOn);
        }

        public async Task<SymmetricKeyResponseDto> GetKeyById(string userId, Guid publicId)
        {
            var key = await _keyRepository.GetByIdAsync(userId, publicId);

            return new SymmetricKeyResponseDto(
                key.PublicId,
                key.Name,
                key.KeyType,
                key.KeyStatus,
                key.KeyLength,
                new EncryptedEvelopeResponseDto(
                    key.EnvelopeVersion,
                    key.WrapAlgorithm,
                    key.WrappingKeyId,
                    key.Ciphertext.ToArray(),
                    key.Tag,
                    key.Nonce),
                key.CreatedOn);
        }

        public async Task<List<SymmetricKeyResponseDto>> GetKeys(string userId)
        {
            var keys = await _keyRepository.GetAllAsync(userId);

            return keys.Select(k => new SymmetricKeyResponseDto(
                PublicId: k.PublicId,
                Name: k.Name,
                KeyType: k.KeyType,
                KeyStatus: k.KeyStatus,
                KeyLength: k.KeyLength,
                new EncryptedEvelopeResponseDto(
                    k.EnvelopeVersion,
                    k.WrapAlgorithm,
                    k.WrappingKeyId,
                    k.Ciphertext.ToArray(),
                    k.Tag,
                    k.Nonce),
                CreatedOn: k.CreatedOn)).ToList();
        }

        public Task<bool> RemoveKeyById(string userId, Guid publicId)
        {
            return _keyRepository.RemoveByIdAsync(userId, publicId);
        }
    }
}
