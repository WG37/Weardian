

namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    public sealed record EncryptedEnvelopeDto(
        Guid EnvelopeId,
        WrappedKeyDto WrappedKey,
        PayloadRecordDto Payload);
}
