using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    public sealed record EncryptedPayloadRecordDto(
        Guid EnvelopeId,
        string Name,
        string Algorithm,
        byte[] Ciphertext,
        byte[] Nonce,
        byte[] Tag,
        DateTime CreatedOn);
}
