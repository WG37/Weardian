using Weardian.Client.Domain.PayloadRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces.Symmetric.Repositories
{
    public interface IPayloadRecordRepository
    {
        public Task AddLocalPayloadRecordAsync(PayloadRecord payloadRecord);
        public Task<IReadOnlyList<PayloadRecord>> GetLocalPayloadRecordsAsync();
        public Task<PayloadRecord> GetLocalPayloadRecordByIdAsync(Guid envelopeId);
        public bool RemoveLocalPayloadRecordById(Guid envelopeId);

    }
}
