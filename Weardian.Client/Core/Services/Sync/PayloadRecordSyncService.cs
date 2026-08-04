using Weardian.Client.Core.DTOs.Sync.Transfers;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Core.Interfaces.Sync;

namespace Weardian.Client.Core.Services.Sync
{
    public class PayloadRecordSyncService : IPayloadRecordSyncService
    {
        private readonly IPayloadRecordRepository _payloadRepo;

        public PayloadRecordSyncService(IPayloadRecordRepository payloadRepo)
        {
            _payloadRepo = payloadRepo;
        }

        public async Task<IReadOnlyList<PayloadRecordTransferDto>> GetAllPayloadRecordsAsync()
        {
            var records = await _payloadRepo.GetLocalPayloadRecordsAsync();

            var payloadRecords = new List<PayloadRecordTransferDto>();

            foreach (var p in records)
            {
                payloadRecords.Add(new PayloadRecordTransferDto(
                    EnvelopeId: p.EnvelopeId,
                    Name: p.Name,
                    KeyType: p.KeyType,
                    EnvelopeVersion: p.Version,
                    Algorithm: p.Algorithm,
                    Nonce: p.Nonce,
                    Ciphertext: p.Ciphertext,
                    Tag: p.Tag));
            }
            return payloadRecords;
        }

        public async Task<PayloadRecordTransferDto> GetPayloadRecordByIdAsync(Guid envelopeId)
        {
            var record = await _payloadRepo.GetLocalPayloadRecordByIdAsync(envelopeId);

            if (record == null)
                throw new InvalidOperationException($"No payload record found for the envlope: ${envelopeId}");

            return new PayloadRecordTransferDto(
                EnvelopeId: record.EnvelopeId,
                Name: record.Name,
                KeyType: record.KeyType,
                EnvelopeVersion: record.Version,
                Algorithm: record.Algorithm,
                Nonce: record.Nonce,
                Ciphertext: record.Ciphertext,
                Tag: record.Tag);
        }
    }
}
