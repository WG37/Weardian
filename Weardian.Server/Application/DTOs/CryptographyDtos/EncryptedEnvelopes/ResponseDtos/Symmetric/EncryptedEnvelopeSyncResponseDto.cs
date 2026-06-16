

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos.Symmetric
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        KeyRecordResponseDto? KeyRecord,
        PayloadRecordResponseDto? PayloadRecord,
        bool Success,
        string? Error
        );
}
