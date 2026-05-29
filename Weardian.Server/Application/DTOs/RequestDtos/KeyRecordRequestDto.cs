using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.RequestDtos
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
