using Weardian.Client.Core.DTOs.Sync.Response;
using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IEnvelopeSyncService
    {
        public Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncDto envelopeRequest);
    }
}
