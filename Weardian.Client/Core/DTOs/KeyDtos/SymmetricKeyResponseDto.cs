using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Domain.Keys;

namespace Weardian.Client.Core.DTOs.KeyDtos
{
    internal sealed record SymmetricKeyResponseDto(
        Guid LocalId,
        string Name,
        KeyType KeyType,
        KeyStatus KeyStatus,
        EncryptedEnvelopeDto Envelope,
        DateTime CreatedOn
        );
}
