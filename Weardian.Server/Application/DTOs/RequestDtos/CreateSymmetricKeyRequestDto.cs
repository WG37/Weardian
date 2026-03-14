using System.ComponentModel.DataAnnotations;
using Weardian.Server.Application.DTOs.CryptographyDto;
using Weardian.Server.Domain.Keys;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public sealed record CreateSymmetricKeyRequestDto(
        [Required, MaxLength(16)]
        string Name,
        [Required] KeyType KeyType,
        [Required] EncryptedEnvelopeRequestDto Envelope);
}
