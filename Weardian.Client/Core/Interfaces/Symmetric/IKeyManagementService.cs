using Weardian.Client.Core.DTOs.MessageHandlerDtos;

namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface IKeyManagementService
    {
        public Task<EncryptionResultDto> CreateEncryptedPasswordAsync(string keyName, string password, bool createSynced = false);
        public Task<string> RetrieveDecryptedPasswordAsync(Guid envelopeId);
    }
}
