namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal sealed record PayloadResult(
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
