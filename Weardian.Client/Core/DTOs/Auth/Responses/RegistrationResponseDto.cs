namespace Weardian.Client.Core.DTOs.Auth.Responses
{
    public sealed record RegistrationResponseDto(
        bool IsSuccessful,
        string? Error
        );
    
}
