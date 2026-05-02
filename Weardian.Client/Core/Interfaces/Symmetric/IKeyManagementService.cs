namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface IKeyManagementService
    {
        public Task CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced = false);
    }
}
