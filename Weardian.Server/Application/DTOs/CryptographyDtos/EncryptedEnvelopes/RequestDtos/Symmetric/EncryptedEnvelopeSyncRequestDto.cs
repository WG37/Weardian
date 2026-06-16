using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric
{
    public sealed record EncryptedEnvelopeSyncRequestDto(
        Guid EnvelopeId,
        KeyRecordRequestDto KeyRequestDto,
        PayloadRecordRequestDto PayloadRequestDto);
}
