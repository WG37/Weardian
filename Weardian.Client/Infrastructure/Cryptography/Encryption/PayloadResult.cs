namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    public sealed record PayloadResult(
        int Version,
        string Algorithm,
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
