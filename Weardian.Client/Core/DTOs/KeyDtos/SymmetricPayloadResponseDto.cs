using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.KeyDtos
{
    internal sealed record SymmetricPayloadResponseDto(
        string Name,
        KeyStatus KeyStatus,
        EncryptedEnvelopeDto Envelope,
        DateTime CreatedOn
        );
}
