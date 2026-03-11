
namespace Weardian.Server.Application.DTOs.CryptographyDto
{
    public sealed record EncryptedEvelopeResponseDto(
        int Version,
        string WrapAlgorithm,
        Guid WrappingKeyId,
        byte[] Ciphertext,
        byte[] Tag,
        byte[] Nonce);
}
