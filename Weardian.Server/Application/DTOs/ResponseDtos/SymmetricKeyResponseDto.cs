using Weardian.Server.Application.DTOs.CryptographyDtos;
using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.ResponseDtos
{
    public sealed record SymmetricKeyResponseDto(
        Guid EnvelopeId,
        string Name,
        KeyType KeyType,
        KeyStatus KeyStatus,
        int KeyLength,
        EncryptedEnvelopeResponseDto Envelope,
        DateTime CreatedOn);
}
