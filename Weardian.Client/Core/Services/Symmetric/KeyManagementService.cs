using Weardian.Client.Core.DTOs.CryptographyDtos;
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
        private readonly IKeyRecordSyncService _keySyncService;
        private readonly IInputValidationService _validationService;
        public KeyManagementService(
            ISymmetricCryptoService symmetricCryptoService,
            IPayloadRecordRepository payloadRecordRepo,
            IKeyRecordRepository keyRecordRepo,
            IKeyRecordSyncService keySyncService,
            IInputValidationService validationService)
        {
            _symmetricCryptoService = symmetricCryptoService;
            _payloadRecordRepo = payloadRecordRepo;
            _keyRecordRepo = keyRecordRepo; 
            _keySyncService = keySyncService;
            _validationService = validationService;
        }

        public async Task CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced)
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
                    var syncResult = await _keySyncService.SyncKeyRecordAsync(keyRecord);

                    keyRecord.IsSynced = true;
                    keyRecord.SyncedOn = syncResult.SyncedOn;

                    await _keyRecordRepo.UpdateLocalKeyRecordByIdAsync(keyRecord);
                }
                catch (Exception ex)
                {
                    keyRecord.IsSynced = false;

                    await _keyRecordRepo.UpdateLocalKeyRecordByIdAsync(keyRecord);

                    throw new InvalidOperationException("Failed to sync key record to server.", ex);
                }
            }
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
