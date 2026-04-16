using Weardian.Server.Application.DTOs.CryptographyDtos;
using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.KeyRecords.Symmetric;
using Weardian.Server.Domain.KeyRecords;

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
            // todo: add validation guards

            if (req.KeyType != KeyType.Encryption &&
                req.KeyType != KeyType.Verification &&
                req.KeyType != KeyType.Signing)

                throw new ArgumentException("Invalid KeyType", nameof(req.KeyType));

            var keyRecord = new SymmetricKeyRecord(req.Envelope.Ciphertext)
            {
                Name = req.Name,
                KeyType = req.KeyType,
                WrapAlgorithm = req.Envelope.WrapAlgorithm,
                WrappingKeyId = req.Envelope.WrappingKeyId,
                WrappedKeyTag = req.Envelope.Tag,
                WrappedKeyNonce = req.Envelope.Nonce,
                UserId = userId
            };

            await _keyRepository.AddAsync(keyRecord);

            return new SymmetricKeyResponseDto(
                keyRecord.EnvelopeId,
                keyRecord.Name,
                keyRecord.KeyType,
                keyRecord.KeyStatus,
                keyRecord.KeyLength,
                new EncryptedEnvelopeResponseDto(
                    keyRecord.EnvelopeVersion,
                    keyRecord.WrapAlgorithm,
                    keyRecord.WrappingKeyId,
                    keyRecord.WrappedKeyCiphertext.ToArray(),
                    keyRecord.WrappedKeyTag,
                    keyRecord.WrappedKeyNonce
            ),
                keyRecord.CreatedOn);
        }

        public async Task<SymmetricKeyResponseDto> GetKeyById(string userId, Guid envelopeId)
        {
            var keyRecord = await _keyRepository.GetByIdAsync(userId, envelopeId);

            return new SymmetricKeyResponseDto(
                keyRecord.EnvelopeId,
                keyRecord.Name,
                keyRecord.KeyType,
                keyRecord.KeyStatus,
                keyRecord.KeyLength,
                new EncryptedEnvelopeResponseDto(
                    keyRecord.EnvelopeVersion,
                    keyRecord.WrapAlgorithm,
                    keyRecord.WrappingKeyId,
                    keyRecord.WrappedKeyCiphertext.ToArray(),
                    keyRecord.WrappedKeyTag,
                    keyRecord.WrappedKeyNonce),
                keyRecord.CreatedOn);
        }

        public async Task<IReadOnlyList<SymmetricKeyResponseDto>> GetKeys(string userId)
        {
            var keyRecords = await _keyRepository.GetAllAsync(userId);

            return keyRecords.Select(k => new SymmetricKeyResponseDto(
                EnvelopeId: k.EnvelopeId,
                Name: k.Name,
                KeyType: k.KeyType,
                KeyStatus: k.KeyStatus,
                KeyLength: k.KeyLength,
                new EncryptedEnvelopeResponseDto(
                    k.EnvelopeVersion,
                    k.WrapAlgorithm,
                    k.WrappingKeyId,
                    k.WrappedKeyCiphertext.ToArray(),
                    k.WrappedKeyTag,
                    k.WrappedKeyNonce),
                CreatedOn: k.CreatedOn)).ToList();
        }

        public Task<bool> RemoveKeyById(string userId, Guid envelopeId)
        {
            return _keyRepository.RemoveByIdAsync(userId, envelopeId);
        }
    }
}
