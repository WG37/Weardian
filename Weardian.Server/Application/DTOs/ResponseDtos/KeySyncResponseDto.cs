namespace Weardian.Server.Application.DTOs.ResponseDtos
{
    public sealed record KeySyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
