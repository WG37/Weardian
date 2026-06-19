namespace Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.RequestDtos
{
    public sealed record EncryptedEnvelopeSyncRequestDto(
        Guid EnvelopeId,
        KeyRecordRequestDto KeyRecord,
        PayloadRecordRequestDto PayloadRecord);
}
