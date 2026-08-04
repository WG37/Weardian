namespace Weardian.Client.Core.DTOs.Sync.Response
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
