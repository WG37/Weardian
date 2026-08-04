namespace Weardian.Client.Core.DTOs.Sync.Transfers
{
    public sealed record EncryptedEnvelopeSyncDto(
        Guid EnvelopeId,
        KeyRecordTransferDto KeyRecord,
        PayloadRecordTransferDto PayloadRecord);
}
