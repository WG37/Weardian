using System.ComponentModel.DataAnnotations;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public record GetSymmetricKeyRequestDto(
        [Required]
        Guid PublicID);
}
