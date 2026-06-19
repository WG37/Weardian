using Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.RequestDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.InputValidation;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords.Symmetric;

namespace Weardian.Client.Core.Services.Symmetric
{
    public sealed class KeyManagementService : IKeyManagementService
    {
        private readonly ISymmetricCryptoService _symmetricCryptoService;
        private readonly IPayloadRecordRepository _payloadRecordRepo;
        private readonly IKeyRecordRepository _keyRecordRepo;
        private readonly IEnvelopeSyncService _envelopeSyncService;
        private readonly IInputValidationService _validationService;
        public KeyManagementService(
            ISymmetricCryptoService symmetricCryptoService,
            IPayloadRecordRepository payloadRecordRepo,
            IKeyRecordRepository keyRecordRepo,
            IEnvelopeSyncService envelopeSyncService,
            IInputValidationService validationService)
        {
            _symmetricCryptoService = symmetricCryptoService;
            _payloadRecordRepo = payloadRecordRepo;
            _keyRecordRepo = keyRecordRepo; 
            _envelopeSyncService = envelopeSyncService;
            _validationService = validationService;
        }

        public async Task<EncryptionResponseDto> CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced)
        {
            var results = _validationService.ValidateEncryptedPassword(keyName, password);

            if (!results.IsValid)    
                throw new ArgumentException(string.Join("\n", results.Errors));

            KeyRecord keyRecord;
            PayloadRecord payloadRecord;
            try
            {
                var envelope = await _symmetricCryptoService.CreateEncryptedEnvelopeAsync(password);

                keyRecord = new KeyRecord(envelope.WrappedKey.WrappedKeyCiphertext)
                {
                    EnvelopeId = envelope.EnvelopeId,
                    EnvelopeVersion = envelope.WrappedKey.Version,
                    Name = keyName,
                    KeyType = Domain.Enums.KeyType.Encryption,
                    WrapAlgorithm = envelope.WrappedKey.WrapAlgorithm,
                    WrappingKeyId = envelope.WrappedKey.WrappingKeyId,
                    WrappedKeyNonce = envelope.WrappedKey.WrappedKeyNonce,
                    WrappedKeyTag = envelope.WrappedKey.WrappedKeyTag
                };

                payloadRecord = new PayloadRecord(envelope.Payload.Ciphertext)
                {
                    EnvelopeId = envelope.EnvelopeId,
                    Version = envelope.Payload.Version,
                    Name = keyName,
                    KeyType = Domain.Enums.KeyType.Encryption,
                    Algorithm = envelope.Payload.Algorithm,
                    Nonce = envelope.Payload.Nonce,
                    Tag = envelope.Payload.Tag
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create envelope.", ex);
            }

            try
            {
                if (keyRecord.EnvelopeId != payloadRecord.EnvelopeId)
                    throw new InvalidOperationException("EnvelopeIds do not match.");

                await _payloadRecordRepo.AddLocalPayloadRecordAsync(payloadRecord);
                await _keyRecordRepo.AddLocalKeyRecordAsync(keyRecord);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save local records to disk.", ex);
            }

            if (createSynced)
            {
                try
                {
                    var envelopeSyncRequest = new EncryptedEnvelopeSyncRequestDto(
                        EnvelopeId: payloadRecord.EnvelopeId,

                        KeyRecord: new KeyRecordRequestDto(
                           EnvelopeId: keyRecord.EnvelopeId,
                           Name: keyRecord.Name,
                           KeyType: keyRecord.KeyType,
                           EnvelopeVersion: keyRecord.EnvelopeVersion,
                           WrapAlgorithm: keyRecord.WrapAlgorithm,
                           WrappingKeyId: keyRecord.WrappingKeyId,
                           WrappedKeyNonce: keyRecord.WrappedKeyNonce,
                           WrappedKeyCiphertext: keyRecord.WrappedKeyCiphertext,
                           WrappedKeyTag: keyRecord.WrappedKeyTag),

                        PayloadRecord: new PayloadRecordRequestDto(
                            EnvelopeId: payloadRecord.EnvelopeId,
                            Name: payloadRecord.Name,
                            KeyType: payloadRecord.KeyType,
                            EnvelopeVersion: payloadRecord.Version,
                            Algorithm: payloadRecord.Algorithm,
                            Nonce: payloadRecord.Nonce,
                            Ciphertext: payloadRecord.Ciphertext,
                            Tag: payloadRecord.Tag)
                        );

                    var envelopeSyncResult = await _envelopeSyncService.SyncEncryptedEnvelopeAsync(envelopeSyncRequest);

                    keyRecord.IsSynced = true;
                    keyRecord.SyncedOn = envelopeSyncResult.SyncedOn;

                    await _keyRecordRepo.UpdateLocalKeyRecordByIdAsync(keyRecord);
                }
                catch (Exception ex)
                {
                    keyRecord.IsSynced = false;

                    await _keyRecordRepo.UpdateLocalKeyRecordByIdAsync(keyRecord);

                    throw new InvalidOperationException("Failed to sync key record to server.", ex);
                }
            }

            return new EncryptionResponseDto(
                KeyId: payloadRecord.EnvelopeId,
                KeyName: payloadRecord.Name,
                Algorithm: payloadRecord.Algorithm,
                KeyType: payloadRecord.KeyType
                );
        }

        public async Task<string> RetrieveDecryptedPasswordAsync(Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
                throw new ArgumentException("PayloadRecordId is invalid.");

            try
            {
                var payloadRecord = await _payloadRecordRepo.GetLocalPayloadRecordByIdAsync(envelopeId);
                var keyRecord = await _keyRecordRepo.GetLocalKeyRecordByIdAsync(envelopeId);

                if (payloadRecord.EnvelopeId != keyRecord.EnvelopeId)
                    throw new InvalidOperationException("EnvelopeIds do not match.");

                return await _symmetricCryptoService.DecryptEncryptedEnvelopeAsync(keyRecord, payloadRecord);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to decrypt records.", ex);
            }
        }
    }
}
