using Weardian.Server.Domain.KeyRecords.Symmetric;
using Weardian.Server.Domain.PayloadRecords.Symmetric;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos
{
    public sealed record EncryptedEnvelopeSyncResponseDto(
        Guid EnvelopeId,
        SymmetricKeyRecord? KeyRecord,
        SymmetricPayloadRecord? PayloadRecord,
        bool Success,
        string? Error
        );
}
