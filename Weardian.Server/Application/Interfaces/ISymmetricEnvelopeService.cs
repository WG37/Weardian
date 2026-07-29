using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric;
using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response;
using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response.Symmetric;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricEnvelopeService
    {
        public Task<EncryptedEnvelopeStatusResponseDto> CreateEncryptedEnvelope(EncryptedEnvelopeSyncRequestDto req, string userId);
        public Task<EncryptedEnvelopeSyncResponseDto> GetEncryptedEnvelopeById(string userId, Guid envelopeId);
        public Task<IReadOnlyList<EncryptedEnvelopeSyncResponseDto>> GetEncryptedEnvelopes(string userId);
        public Task<EncryptedEnvelopeSyncResponseDto> RemoveEncryptedEnvelopeById(string userId, Guid envelopeId);
    }
}
