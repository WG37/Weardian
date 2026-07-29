using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.EnvelopeSyncing.Request
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
