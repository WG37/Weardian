using Weardian.Client.Core.DTOs.MessageHandler.HandleEncryption;

namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface IKeyManagementService
    {
        public Task<EncryptionResponseDto> CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced = false);
        public Task<string> RetrieveDecryptedPasswordAsync(Guid envelopeId);
    }
}
