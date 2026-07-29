namespace Weardian.Client.Core.DTOs.Cryptography
{
    public sealed record WrappedKeyDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag,
        byte[] WrappedKeyNonce);
}
