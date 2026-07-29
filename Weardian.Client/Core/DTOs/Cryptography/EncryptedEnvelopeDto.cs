namespace Weardian.Client.Core.DTOs.Cryptography
{
    public sealed record EncryptedEnvelopeDto(
        Guid EnvelopeId,
        WrappedKeyDto WrappedKey,
        PayloadRecordDto Payload);
}
