namespace Weardian.Client.Core.DTOs.EnvelopeSyncing.Response
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        DateTime SyncedOn);
}
