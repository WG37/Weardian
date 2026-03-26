namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    internal sealed record PayloadRecordDto(
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
