using System.ComponentModel.DataAnnotations;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public sealed record GetSymmetricKeyRequestDto(
        [Required]
        Guid EnvelopeId);
}
