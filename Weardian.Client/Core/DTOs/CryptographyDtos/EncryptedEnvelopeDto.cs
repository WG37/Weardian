

namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    internal sealed record EncryptedEnvelopeDto(
        Guid EnvelopeId,
        WrappedKeyDto WrappedKey,
        PayloadRecordDto EncryptedData);
}
