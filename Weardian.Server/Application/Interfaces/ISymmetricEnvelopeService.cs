using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric;
using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos;
using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos.Symmetric;

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
