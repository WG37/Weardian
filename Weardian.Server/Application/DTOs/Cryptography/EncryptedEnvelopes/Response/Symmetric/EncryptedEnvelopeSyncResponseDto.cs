namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response.Symmetric
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        KeyRecordResponseDto? KeyRecord,
        PayloadRecordResponseDto? PayloadRecord,
        bool Success,
        string? Error
        );
}
