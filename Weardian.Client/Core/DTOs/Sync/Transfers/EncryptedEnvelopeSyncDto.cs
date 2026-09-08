namespace Weardian.Client.Core.DTOs.Sync.Transfers
{
    public sealed record EncryptedEnvelopeSyncDto(
        Guid EnvelopeId,
        KeyRecordTransferDto KeyRequestDto,
        PayloadRecordTransferDto PayloadRequestDto);
}
