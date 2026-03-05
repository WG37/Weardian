
namespace Weardian.Server.Application.DTOs.CryptographyDto
{
    public sealed record EncryptedEvelopeDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] Tag,
        byte[] Nonce,
        byte[] CipherText);
}
