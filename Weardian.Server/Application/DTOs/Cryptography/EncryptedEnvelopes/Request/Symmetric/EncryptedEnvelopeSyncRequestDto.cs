using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric
{
    public sealed record EncryptedEnvelopeSyncRequestDto(
        Guid EnvelopeId,
        KeyRecordRequestDto KeyRequestDto,
        PayloadRecordRequestDto PayloadRequestDto);
}
