using System.ComponentModel.DataAnnotations;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public record RemoveSymmetricKeyRequestDto(
        [Required]
        Guid PublicId);
}
