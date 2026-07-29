namespace Weardian.Client.Core.DTOs.Auth.Responses
{
     public sealed record LoginResponseDto(
         bool IsSuccessful,
         string? Error
         );
}
