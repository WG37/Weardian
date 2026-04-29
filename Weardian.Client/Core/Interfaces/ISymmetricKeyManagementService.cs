namespace Weardian.Client.Core.Interfaces
{
    public interface ISymmetricKeyManagementService
    {
        public Task CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced = false);
    }
}
