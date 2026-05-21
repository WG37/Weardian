using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public sealed record KeySyncRequestDto(
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
