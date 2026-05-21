using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyService
    {
        public Task<SymmetricKeyResponseDto> CreateKey(KeySyncRequestDto keyBytes, string userId);
        public Task<SymmetricKeyResponseDto> GetKeyById(string userId, Guid envelopeId);
        public Task<IReadOnlyList<SymmetricKeyResponseDto>> GetKeys(string userId);
        public Task<bool> RemoveKeyById(string userId, Guid envelopeId);
    }
}
