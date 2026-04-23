using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyRepository
    {
        public Task AddLocalRecordsAsync(SymmetricKeyRecord keyRecord, PayloadRecord payloadRecord);
        public Task<IReadOnlyList<PayloadRecord>> GetLocalPayloadRecordsAsync();
        public Task<PayloadRecord> GetLocalPayloadRecordByIdAsync(Guid envelopeId);
        public bool RemoveLocalPayloadRecordById(Guid envelopeId);

    }
}
