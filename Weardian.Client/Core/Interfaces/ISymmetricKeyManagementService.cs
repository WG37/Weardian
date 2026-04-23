using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyManagementService
    {
        public Task CreateEncryptedPasswordAsync(string keyName, string password);
        public Task<IReadOnlyList<EncryptedPayloadRecordDto>> GetPayloadRecordsAsync();
        public Task<EncryptedPayloadRecordDto> GetPayloadRecordByIdAsync(Guid envelopeId);
        public bool RemoveRecordById(Guid envelopeId);
    }
}
