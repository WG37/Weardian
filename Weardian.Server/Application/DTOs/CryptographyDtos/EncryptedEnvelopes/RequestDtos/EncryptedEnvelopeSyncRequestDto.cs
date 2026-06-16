using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos
{
    public sealed record EncryptedEnvelopeSyncRequestDto(
        Guid EnvelopeId,
        KeyRecordRequestDto KeyRequestDto,
        PayloadRecordRequestDto PayloadRequestDto);
}
