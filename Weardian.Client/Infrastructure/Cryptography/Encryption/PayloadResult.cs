namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal sealed record PayloadResult(
        int Version,
        string Algorithm,
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
