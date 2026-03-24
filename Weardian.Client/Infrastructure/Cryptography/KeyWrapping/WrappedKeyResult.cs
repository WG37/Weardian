namespace Weardian.Client.Infrastructure.Cryptography.KeyWrapping
{
    internal sealed record WrappedKeyResult(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] WrappedKeyCiphertext,
        byte[] WrappedKeyTag,
        byte[] WrappedKeyNonce);
}
