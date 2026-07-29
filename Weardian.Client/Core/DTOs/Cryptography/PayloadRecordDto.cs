namespace Weardian.Client.Core.DTOs.Cryptography
{
    public sealed record PayloadRecordDto(
        int Version,
        string Algorithm,
        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
