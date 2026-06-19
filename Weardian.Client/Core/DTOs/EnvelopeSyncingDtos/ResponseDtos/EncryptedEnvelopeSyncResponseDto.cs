namespace Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.ResponseDtos
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
