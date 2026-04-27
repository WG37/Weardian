namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    public sealed record WrappedKeyDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag,
        byte[] WrappedKeyNonce);
}
