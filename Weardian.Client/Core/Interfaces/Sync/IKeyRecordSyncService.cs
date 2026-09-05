using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IKeyRecordSyncService
    {
        public Task<IReadOnlyList<KeyRecordTransferDto>> GetAllKeyRecordsAsync();
        public Task<KeyRecordTransferDto> GetKeyRecordByIdAsync(Guid envelopeId);
        public Task AddKeyRecordAsync(KeyRecordTransferDto keyRecordDto);
    }
}
