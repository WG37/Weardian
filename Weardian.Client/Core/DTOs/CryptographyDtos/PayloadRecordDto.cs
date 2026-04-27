namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    public sealed record PayloadRecordDto(
        int Version,
        string Algorithm,
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
