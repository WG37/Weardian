using Weardian.Server.Application.DTOs.CryptographyDtos;
using Weardian.Server.Application.Interfaces;
using Weardian.Server.Domain.EncryptedEnvelopes.Symmetric;
using Weardian.Server.Domain.KeyRecords.Symmetric;
using Weardian.Server.Domain.PayloadRecords.Symmetric;

namespace Weardian.Server.Application.Services.SymmetricServices
{
    public class SymmetricEnvelopeService : ISymmetricEnvelopeService
    {
        private readonly ISymmetricEnvelopeRepository _envelopeRepository;
        private readonly IEnvelopeValidationService _envelopeValidation;

        public SymmetricEnvelopeService(
            ISymmetricEnvelopeRepository keyRepository,
            IEnvelopeValidationService envelopeValidation)
        {
            _envelopeRepository = keyRepository;
            _envelopeValidation = envelopeValidation;
        }

        public async Task<EncryptedEnvelopeResponseDto> CreateKey(EncryptedEnvelopeSyncRequestDto req, string userId)
        {
            var results = _envelopeValidation.ValidateEnvelope(req);
           
            if (!results.IsValid)
            {
                throw new ArgumentException(string.Join("\n", results.Errors), nameof(req));
            }

            var keyRecord = new SymmetricKeyRecord(req.KeyRequestDto.WrappedKeyCiphertext)
            {
                EnvelopeId = req.EnvelopeId,
                Name = req.KeyRequestDto.Name,
                KeyType = req.KeyRequestDto.KeyType,
                WrapAlgorithm = req.KeyRequestDto.WrapAlgorithm,
                WrappingKeyId = req.KeyRequestDto.WrappingKeyId,
                WrappedKeyTag = req.KeyRequestDto.WrappedKeyTag,
                WrappedKeyNonce = req.KeyRequestDto.WrappedKeyNonce,
            };

            var payloadRecord = new SymmetricPayloadRecord(req.PayloadRequestDto.Ciphertext)
            {
                EnvelopeId = req.EnvelopeId,
                Name = req.PayloadRequestDto.Name,
                KeyType = req.PayloadRequestDto.KeyType,
                Algorithm = req.PayloadRequestDto.Algorithm,
                Nonce = req.PayloadRequestDto.Nonce,
                Tag = req.PayloadRequestDto.Tag,
            };

            var encryptedEnvelope = new SymmetricEncryptedEnvelope()
            {
                EnvelopeId = req.EnvelopeId,
                KeyRecord = keyRecord,
                PayloadRecord = payloadRecord,
                UserId = userId
            };

            await _envelopeRepository.AddAsync(encryptedEnvelope);

            return new SymmetricKeyResponseDto(
                keyRecord.EnvelopeId,
                keyRecord.Name,
                keyRecord.KeyType,
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
            var keyRecord = await _envelopeRepository.GetByIdAsync(userId, envelopeId);

            return new SymmetricKeyResponseDto(
                keyRecord.EnvelopeId,
                keyRecord.Name,
                keyRecord.KeyType,
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
            var keyRecords = await _envelopeRepository.GetAllAsync(userId);

            return keyRecords.Select(k => new SymmetricKeyResponseDto(
                EnvelopeId: k.EnvelopeId,
                Name: k.Name,
                KeyType: k.KeyType,
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
            return _envelopeRepository.RemoveByIdAsync(userId, envelopeId);
        }
    }
}
