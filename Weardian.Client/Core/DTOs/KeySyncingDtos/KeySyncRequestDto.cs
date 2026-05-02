namespace Weardian.Client.Core.DTOs.KeySyncingDtos
{
    public sealed record KeySyncRequestDto(
        Guid EnvelopeId,
        string Name,
        int EnvelopeVersion,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] WrappedKeyNonce,
        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag,
        DateTime CreatedOn);
}
