using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IPayloadRecordSyncService
    {
        public Task<IReadOnlyList<PayloadRecordTransferDto>> GetAllPayloadRecordsAsync();
        public Task<PayloadRecordTransferDto> GetPayloadRecordByIdAsync(Guid envelopeId);
    }
}
