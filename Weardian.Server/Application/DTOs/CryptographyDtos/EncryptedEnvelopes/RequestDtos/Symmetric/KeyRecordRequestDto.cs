using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric
{
    public sealed record KeyRecordRequestDto(
        Guid EnvelopeId,
        string Name,
        KeyType KeyType,
        int EnvelopeVersion,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] WrappedKeyNonce,
        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag);
}
