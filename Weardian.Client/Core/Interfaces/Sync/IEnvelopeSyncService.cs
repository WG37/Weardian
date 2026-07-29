using Weardian.Client.Core.DTOs.EnvelopeSyncing.Request;
using Weardian.Client.Core.DTOs.EnvelopeSyncing.Response;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IEnvelopeSyncService
    {
        public Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncRequestDto envelopeRequest);
    }
}
