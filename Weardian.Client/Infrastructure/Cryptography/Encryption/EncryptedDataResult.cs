namespace Weardian.Client.Infrastructure.Cryptography.Encryption
{
    internal sealed record EncryptedDataResult(
        byte[] Nonce,
        byte[] Tag,
        byte[] Ciphertext);
}
