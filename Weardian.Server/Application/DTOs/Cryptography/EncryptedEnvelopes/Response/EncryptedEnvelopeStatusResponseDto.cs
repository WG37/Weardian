namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response
{
    public sealed record EncryptedEnvelopeStatusResponseDto(
        Guid EnvelopeId,
        string? Name,
        bool Success,
        string? Error,
        DateTime? SyncedOn);
}
