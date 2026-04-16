namespace Weardian.Server.Application.DTOs.CryptographyDtos
{
    public sealed record EncryptedEnvelopeResponseDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
