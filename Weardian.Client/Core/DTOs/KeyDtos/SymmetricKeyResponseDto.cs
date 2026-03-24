using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Domain.KeyRecords;

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
