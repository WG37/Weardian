namespace Weardian.Client.Core.DTOs.EnvelopeSyncing.Request
{
    public sealed record EncryptedEnvelopeSyncRequestDto(
        Guid EnvelopeId,
        KeyRecordRequestDto KeyRecord,
        PayloadRecordRequestDto PayloadRecord);
}
