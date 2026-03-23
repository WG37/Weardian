namespace Weardian.Server.Application.DTOs.CryptographyDtos
{
    public sealed record EncryptedEnvelopeRequestDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
