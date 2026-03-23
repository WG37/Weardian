namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    internal sealed record EncryptedEnvelopeDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,

        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag,
        byte[] WrappedKeyNonce,

        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
