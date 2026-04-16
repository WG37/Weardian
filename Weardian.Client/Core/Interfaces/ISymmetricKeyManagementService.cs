using Weardian.Client.Core.DTOs.KeyDtos;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyManagementService
    {
        public Task CreateEncryptedPasswordAsync(string keyName, string password);
        public Task<SymmetricPayloadResponseDto> GetKeyByIdAsync(Guid localId);
        public Task<SymmetricPayloadResponseDto> GetKeysAsync();
        public Task<bool> RemoveKeyById(Guid localId);
    }
}
