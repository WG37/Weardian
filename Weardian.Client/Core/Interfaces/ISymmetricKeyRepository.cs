using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords;

namespace Weardian.Client.Core.Interfaces
{
    internal interface ISymmetricKeyRepository
    {
        public Task AddLocalRecordsAsync(SymmetricKeyRecord keyRecord, PayloadRecord payloadRecord);
        public Task<IReadOnlyList<EncryptedPayloadRecordDto>> GetLocalPayloadRecordsAsync();
        public Task<EncryptedPayloadRecordDto> GetLocalPayloadRecordById(Guid payloadId);
        public bool RemoveLocalPayloadRecordById(Guid payloadId);

    }
}
