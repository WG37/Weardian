using Weardian.Client.Core.DTOs.KeyDtos;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyManagementService
    {
        public Task CreateSymmetricKeyAsync(string password);
        public Task<SymmetricKeyResponseDto> GetKeyByIdAsync(Guid localId);
        public Task<SymmetricKeyResponseDto> GetKeysAsync();
        public Task<bool> RemoveKeyById(Guid localId);
    }
}
