namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos
{
    public sealed record EncryptedEnvelopeStatusResponseDto(
        Guid EnvelopeId,
        string? Name,
        bool Success,
        string? Error,
        DateTime? SyncedOn);
}
