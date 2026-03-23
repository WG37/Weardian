using Weardian.Server.Application.DTOs.CryptographyDtos;
using Weardian.Server.Domain.Keys;

namespace Weardian.Server.Application.DTOs.ResponseDtos
{
    public sealed record SymmetricKeyResponseDto(
        Guid PublicId,
        string Name,
        KeyType KeyType,
        KeyStatus KeyStatus,
        int KeyLength,
        EncryptedEvelopeResponseDto Envelope,
        DateTime CreatedOn);
}
