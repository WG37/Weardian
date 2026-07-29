using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric
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
