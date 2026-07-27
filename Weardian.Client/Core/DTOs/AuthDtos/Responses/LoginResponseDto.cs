namespace Weardian.Client.Core.DTOs.AuthDtos.Responses
{
     public sealed record LoginResponseDto(
         bool IsSuccessful,
         string? Error
         );
}
