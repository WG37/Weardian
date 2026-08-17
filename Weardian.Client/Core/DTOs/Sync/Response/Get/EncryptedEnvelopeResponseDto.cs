using Weardian.Client.Core.DTOs.Sync.Transfers;

namespace Weardian.Client.Core.DTOs.Sync.Response.Get
{
    public sealed record EncryptedEnvelopeResponseDto(
        Guid EnvelopeId,
        KeyRecordTransferDto? KeyRecord,
        PayloadRecordTransferDto? PayloadRecord,
        bool Success,
        string? Error
        );
}
