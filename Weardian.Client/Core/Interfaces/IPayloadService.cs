using Weardian.Client.Core.DTOs.CryptographyDtos;

namespace Weardian.Client.Core.Interfaces
{
    public interface IPayloadService
    {
        public Task<IReadOnlyList<EncryptedPayloadRecordDto>> GetPayloadRecordsAsync();
        public Task<EncryptedPayloadRecordDto> GetPayloadRecordByIdAsync(Guid envelopeId);
        public bool RemoveRecordById(Guid envelopeId);
    }
}
