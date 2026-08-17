namespace Weardian.Client.Core.DTOs.Sync.Response.Post
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
