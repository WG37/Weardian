namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    public sealed record PayloadRecordDto(
        int Version,
        string Algorithm,
        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
