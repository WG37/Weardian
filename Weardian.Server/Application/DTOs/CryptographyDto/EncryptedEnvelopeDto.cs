
namespace Weardian.Server.Application.DTOs.CryptographyDto
{
    public sealed record EncryptedEvelopeDto(
        int Version,
        string WrapAlgorithm,
        string WrappingKeyId,
        byte[] Tag,
        byte[] Nonce,
        byte[] CipherText);
}
