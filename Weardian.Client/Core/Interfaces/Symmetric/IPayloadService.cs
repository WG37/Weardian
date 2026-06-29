using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos;

namespace Weardian.Client.Core.Interfaces.Symmetric
{
    public interface IPayloadService
    {
        public Task<IReadOnlyList<RetrievePayloadResponseDto>> GetPayloadRecordsAsync();
        public Task<RetrievePayloadResponseDto> GetPayloadRecordByIdAsync(Guid envelopeId);
        public bool RemoveRecordsById(Guid envelopeId);
    }
}
