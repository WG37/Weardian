using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Application.DTOs.ResponseDtos;

namespace Weardian.Server.Application.Interfaces
{
    public interface ISymmetricKeyService
    {
        public Task<SymmetricKeyResponseDto> CreateKey(CreateSymmetricKeyRequestDto keyBytes);
        public Task<SymmetricKeyResponseDto> GetKeyById(Guid publicId);
        public Task<IEnumerable<SymmetricKeyResponseDto>> GetKeys();
        public Task<bool> RemoveKeyById(Guid publicId);
        public Task<bool> RemoveKeys();
    }
}
