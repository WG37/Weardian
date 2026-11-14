using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weardian.Client.Core.DTOs.Sync.Response.Get;
using Weardian.Client.Core.DTOs.Sync.Response.Post;
using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.Interfaces.APIClient.Sync
{
    public interface IEnvelopeAPIClient
    {
        public Task<EncryptedEnvelopeSyncResponseDto> SyncEncryptedEnvelopeAsync(EncryptedEnvelopeSyncDto envelopeRequest);
        public Task<IReadOnlyList<EncryptedEnvelopeResponseDto>> GetSymmetricServerEnvelopesAsync();
        public Task DeleteSyncedEnvelopeAsync(Guid envelopeId);
    }
}
