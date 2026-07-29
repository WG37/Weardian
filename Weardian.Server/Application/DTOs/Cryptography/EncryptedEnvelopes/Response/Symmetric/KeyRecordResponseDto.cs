using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Response.Symmetric
{
    public sealed record KeyRecordResponseDto(
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
