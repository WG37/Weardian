using Weardian.Client.Core.DTOs.KeySyncingDtos;
using Weardian.Client.Domain.KeyRecords.Symmetric;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IKeyRecordSyncService
    {
        public Task<KeySyncResponseDto> SyncKeyRecordAsync(SymmetricKeyRecord keyRecord);
    }
}
