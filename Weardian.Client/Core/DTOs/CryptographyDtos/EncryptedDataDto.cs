namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    internal sealed record EncryptedDataDto(
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
