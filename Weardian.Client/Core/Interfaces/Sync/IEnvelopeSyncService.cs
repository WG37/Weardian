using Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.RequestDtos;
using Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.ResponseDtos;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IEnvelopeSyncService
    {
        public Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncRequestDto envelopeRequest);
    }
}
