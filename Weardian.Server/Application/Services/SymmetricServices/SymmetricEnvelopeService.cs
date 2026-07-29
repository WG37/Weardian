using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric;
using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response;
using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response.Symmetric;
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

        public async Task<EncryptedEnvelopeStatusResponseDto> CreateEncryptedEnvelope(EncryptedEnvelopeSyncRequestDto req, string userId)
        { 
            var results = _envelopeValidation.ValidateEnvelope(req);
           
            if (!results.IsValid)
            {
                return new EncryptedEnvelopeStatusResponseDto(
                    EnvelopeId: req.EnvelopeId,
                    Name: req.KeyRequestDto.Name,
                    Success: false,
                    Error: string.Join("\n", results.Errors),
                    SyncedOn: null);
            }

            try { 
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

                return new EncryptedEnvelopeStatusResponseDto(
                    EnvelopeId: encryptedEnvelope.EnvelopeId,
                    Name: encryptedEnvelope.KeyRecord.Name,
                    Success: true,
                    Error: null,
                    SyncedOn: encryptedEnvelope.KeyRecord.CreatedOn);
            }
            catch (Exception ex)
            {
                return new EncryptedEnvelopeStatusResponseDto(
                    EnvelopeId: req.EnvelopeId,
                    Name: req.KeyRequestDto.Name,
                    Success: false,
                    Error: ex.Message,
                    SyncedOn: null);
            }
        }

        public async Task<EncryptedEnvelopeSyncResponseDto> GetEncryptedEnvelopeById(string userId, Guid envelopeId)
        {

            if (envelopeId == Guid.Empty)
            {
                return new EncryptedEnvelopeSyncResponseDto(
                    EnvelopeId: envelopeId,
                    KeyRecord: null,
                    PayloadRecord: null,
                    Success: false,
                    Error: "envelopeId cannot be empty");
            }
                

            var envelope = await _envelopeRepository.GetByIdAsync(userId, envelopeId);

            if (envelope == null)
            {
                return new EncryptedEnvelopeSyncResponseDto(
                   EnvelopeId: envelopeId,
                   KeyRecord: null,
                   PayloadRecord: null,
                   Success: false,
                   Error: "Envelope Id is invalid");
            }

            return new EncryptedEnvelopeSyncResponseDto(
                EnvelopeId: envelope.EnvelopeId,
                KeyRecord: new KeyRecordResponseDto(
                    EnvelopeId: envelope.KeyRecord.EnvelopeId,
                    Name: envelope.KeyRecord.Name,
                    KeyType: envelope.KeyRecord.KeyType,
                    EnvelopeVersion: envelope.KeyRecord.EnvelopeVersion,
                    WrapAlgorithm: envelope.KeyRecord.WrapAlgorithm,
                    WrappingKeyId: envelope.KeyRecord.WrappingKeyId,
                    WrappedKeyNonce: envelope.KeyRecord.WrappedKeyNonce,
                    WrappedKeyCiphertext: envelope.KeyRecord.WrappedKeyCiphertext.ToArray(),
                    WrappedKeyTag: envelope.KeyRecord.WrappedKeyTag),

                PayloadRecord: new PayloadRecordResponseDto(
                    EnvelopeId: envelope.PayloadRecord.EnvelopeId,
                    Name: envelope.PayloadRecord.Name,
                    KeyType: envelope.PayloadRecord.KeyType,
                    EnvelopeVersion: envelope.PayloadRecord.EnvelopeVersion,
                    Algorithm: envelope.PayloadRecord.Algorithm,
                    Nonce: envelope.PayloadRecord.Nonce,
                    Ciphertext: envelope.PayloadRecord.Ciphertext.ToArray(),
                    Tag: envelope.PayloadRecord.Tag),

                Success: true,
                Error: null); 
        }

        public async Task<IReadOnlyList<EncryptedEnvelopeSyncResponseDto>> GetEncryptedEnvelopes(string userId)
        {
            
            var envelopes = await _envelopeRepository.GetAllAsync(userId);

            return envelopes.Select(e => new EncryptedEnvelopeSyncResponseDto(
                EnvelopeId: e.EnvelopeId,
                KeyRecord: new KeyRecordResponseDto(
                    EnvelopeId: e.KeyRecord.EnvelopeId,
                    Name: e.KeyRecord.Name,
                    KeyType: e.KeyRecord.KeyType,
                    EnvelopeVersion: e.KeyRecord.EnvelopeVersion,
                    WrapAlgorithm: e.KeyRecord.WrapAlgorithm,
                    WrappingKeyId: e.KeyRecord.WrappingKeyId,
                    WrappedKeyNonce: e.KeyRecord.WrappedKeyNonce,
                    WrappedKeyCiphertext: e.KeyRecord.WrappedKeyCiphertext.ToArray(),
                    WrappedKeyTag: e.KeyRecord.WrappedKeyTag),

                PayloadRecord: new PayloadRecordResponseDto(
                    EnvelopeId: e.PayloadRecord.EnvelopeId,
                    Name: e.PayloadRecord.Name,
                    KeyType: e.PayloadRecord.KeyType,
                    EnvelopeVersion: e.PayloadRecord.EnvelopeVersion,
                    Algorithm: e.PayloadRecord.Algorithm,
                    Nonce: e.PayloadRecord.Nonce,
                    Ciphertext: e.PayloadRecord.Ciphertext.ToArray(),
                    Tag: e.PayloadRecord.Tag
                    ),
                Success: true,
                Error: null)
            ).ToList();
        }

        public async Task<EncryptedEnvelopeSyncResponseDto> RemoveEncryptedEnvelopeById(string userId, Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
            {
                return new EncryptedEnvelopeSyncResponseDto(
                    EnvelopeId: envelopeId,
                    KeyRecord: null,
                    PayloadRecord: null,
                    Success: false,
                    Error: "Envelope Id is invalid");
            }

            var deleted = await _envelopeRepository.RemoveByIdAsync(userId, envelopeId);

            return new EncryptedEnvelopeSyncResponseDto(
                EnvelopeId: envelopeId,
                KeyRecord: null,
                PayloadRecord: null,
                Success: deleted,
                Error: deleted ? null : "Failed to successfully delete envelope");
        }
    }
}
