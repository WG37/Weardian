using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces.Symmetric.Repositories
{
    public interface IKeyRecordRepository
    {
        public Task AddLocalKeyRecordAsync(KeyRecord keyRecord);
        public Task<IReadOnlyList<KeyRecord>> GetLocalKeyRecordsAsync();
        public Task<KeyRecord> GetLocalKeyRecordByIdAsync(Guid envelopeId);
        public Task UpdateLocalKeyRecordByIdAsync(KeyRecord keyRecord);
        public bool RemoveLocalKeyRecordById(Guid envelopeId);
    }
}
