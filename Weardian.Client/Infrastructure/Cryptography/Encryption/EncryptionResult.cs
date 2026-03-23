namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal sealed record EncryptionResult(
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
