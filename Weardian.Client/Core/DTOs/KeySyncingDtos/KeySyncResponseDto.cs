namespace Weardian.Client.Core.DTOs.KeySyncingDtos
{
    public sealed record KeySyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
