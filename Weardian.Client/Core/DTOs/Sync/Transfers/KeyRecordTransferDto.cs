using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.Sync.Transfers
{
    public sealed record KeyRecordTransferDto(
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
