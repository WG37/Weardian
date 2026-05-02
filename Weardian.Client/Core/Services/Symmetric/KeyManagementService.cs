using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords;

namespace Weardian.Client.Core.Services.Symmetric
{
    public sealed class KeyManagementService : IKeyManagementService
    {
        private readonly ISymmetricCryptoService _symmetricCryptoService;
        private readonly IPayloadRecordRepository _payloadRecordRepo;
        private readonly IKeyRecordRepository _keyRecordRepo;
        private readonly IKeyRecordSyncService _keySyncService;
        public KeyManagementService(
            ISymmetricCryptoService symmetricCryptoService,
            IPayloadRecordRepository payloadRecordRepo,
            IKeyRecordRepository keyRecordRepo,
            IKeyRecordSyncService keySyncService)
        {
            _symmetricCryptoService = symmetricCryptoService;
            _payloadRecordRepo = payloadRecordRepo;
            _keyRecordRepo = keyRecordRepo; 
            _keySyncService = keySyncService;
        }

        public async Task CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced)
        {
            if (string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("KeyName or Password input is null, empty or whitespace");

            if (keyName.Length < 3 || keyName.Length > 12)
                throw new ArgumentOutOfRangeException(nameof(keyName),
                    "KeyName must have a minimum of 3 and max of 12 characters.");

            if (password.Length < 8)
                throw new ArgumentOutOfRangeException(nameof(password),
                    "Password must have a length of 8 or more characters.");

            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);

            if (!hasUpper || !hasLower || !hasDigit)
                throw new ArgumentException(
                    "Password must contain at least one upper, lower and a digit.");

            SymmetricKeyRecord keyRecord;
            PayloadRecord payloadRecord;
            try
            {
                var envelope = await _symmetricCryptoService.CreateEncryptedEnvelopeAsync(password);

                keyRecord = new SymmetricKeyRecord(envelope.WrappedKey.WrappedKeyCiphertext)
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
    }
}
