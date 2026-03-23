using System.ComponentModel.DataAnnotations;

namespace Weardian.Server.Application.DTOs.RequestDtos
{
    public sealed record RemoveSymmetricKeyRequestDto(
        [Required]
        Guid PublicId);
}
