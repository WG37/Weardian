using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces.Symmetric.Repositories
{
    public interface IKeyRecordRepository
    {
        public Task AddLocalKeyRecordAsync(SymmetricKeyRecord keyRecord);
        public Task<IReadOnlyList<SymmetricKeyRecord>> GetLocalKeyRecordsAsync();
        public Task<SymmetricKeyRecord> GetLocalKeyRecordByIdAsync(Guid envelopeId);
        public Task UpdateLocalKeyRecordByIdAsync(SymmetricKeyRecord keyRecord);
        public bool RemoveLocalKeyRecordById(Guid envelopeId);
    }
}
