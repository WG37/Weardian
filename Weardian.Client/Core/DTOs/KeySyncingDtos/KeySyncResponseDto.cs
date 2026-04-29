namespace Weardian.Client.Core.DTOs.KeySyncingDtos
{
    public record KeySyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
