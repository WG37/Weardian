using Weardian.Server.Application.DTOs.CryptographyDtos;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricEnvelopeService
    {
        public Task<EncryptedEnvelopeResponseDto> CreateKey(EncryptedEnvelopeSyncRequestDto req, string userId);
        public Task<EncryptedEnvelopeResponseDto> GetKeyById(string userId, Guid envelopeId);
        public Task<IReadOnlyList<EncryptedEnvelopeResponseDto>> GetKeys(string userId);
        public Task<bool> RemoveKeyById(string userId, Guid envelopeId);
    }
}
