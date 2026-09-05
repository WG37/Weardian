using Weardian.Client.Core.DTOs.Sync.Transfers;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Services.Sync
{
    public class KeyRecordSyncService : IKeyRecordSyncService
    {
        private readonly IKeyRecordRepository _keyRepo;

        public KeyRecordSyncService(IKeyRecordRepository keyRepo)
        {
            _keyRepo = keyRepo;
        }

        public async Task<IReadOnlyList<KeyRecordTransferDto>> GetAllKeyRecordsAsync()
        {
            var records = await _keyRepo.GetLocalKeyRecordsAsync();

            var keyRecords = new List<KeyRecordTransferDto>();

            foreach (var k in records)
            {
                keyRecords.Add(new KeyRecordTransferDto(
                    EnvelopeId: k.EnvelopeId,
                    Name: k.Name,
                    KeyType: k.KeyType,
                    EnvelopeVersion: k.EnvelopeVersion,
                    WrapAlgorithm: k.WrapAlgorithm,
                    WrappingKeyId: k.WrappingKeyId,
                    WrappedKeyNonce: k.WrappedKeyNonce,
                    WrappedKeyCiphertext: k.WrappedKeyCiphertext,
                    WrappedKeyTag: k.WrappedKeyTag
                    ));
            }

            return keyRecords;
        }

        public async Task<KeyRecordTransferDto> GetKeyRecordByIdAsync(Guid envelopeId)
        {
            var record = await _keyRepo.GetLocalKeyRecordByIdAsync(envelopeId);

            if (record == null)
                throw new InvalidOperationException($"No key record found for the envelope: {envelopeId}");

            return new KeyRecordTransferDto(
                EnvelopeId: record.EnvelopeId,
                Name: record.Name,
                KeyType: record.KeyType,
                EnvelopeVersion: record.EnvelopeVersion,
                WrapAlgorithm: record.WrapAlgorithm,
                WrappingKeyId: record.WrappingKeyId,
                WrappedKeyNonce: record.WrappedKeyNonce,
                WrappedKeyCiphertext: record.WrappedKeyCiphertext,
                WrappedKeyTag: record.WrappedKeyTag
                );
        }

        public async Task AddKeyRecordAsync(KeyRecordTransferDto keyRecordDto)
        {
            if (keyRecordDto == null)
                throw new ArgumentNullException(nameof(keyRecordDto), "Key record cannot be null");

            var keyRecord = new KeyRecord(keyRecordDto.WrappedKeyCiphertext)
            {
                EnvelopeId = keyRecordDto.EnvelopeId,
                Name = keyRecordDto.Name,
                KeyType = keyRecordDto.KeyType,
                EnvelopeVersion = keyRecordDto.EnvelopeVersion,
                WrapAlgorithm = keyRecordDto.WrapAlgorithm,
                WrappingKeyId = keyRecordDto.WrappingKeyId,
                WrappedKeyNonce = keyRecordDto.WrappedKeyNonce,
                WrappedKeyTag = keyRecordDto.WrappedKeyTag
            };

            await _keyRepo.AddLocalKeyRecordAsync(keyRecord);
        }
    }
}
