namespace Weardian.Client.Core.DTOs.AuthDtos.Responses
{
    public sealed record RegistrationResponseDto(
        bool IsSuccessful,
        string? Error
        );
    
}
