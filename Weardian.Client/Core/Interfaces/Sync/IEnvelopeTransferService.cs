using Weardian.Client.Core.DTOs.Sync.Response.Post;
using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.Interfaces.Sync
{
    public interface IEnvelopeTransferService
    {
        public Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncDto envelopeRequest);
        public Task SyncAllEnvelopesAsync();
    }
}
