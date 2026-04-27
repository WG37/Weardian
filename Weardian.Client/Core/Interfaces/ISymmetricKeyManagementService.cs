using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces
{
    public interface ISymmetricKeyManagementService
    {
        public Task CreateEncryptedPasswordAsync(string keyName, string password);
    }
}
