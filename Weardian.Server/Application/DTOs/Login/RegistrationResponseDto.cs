namespace Weardian.Server.Application.DTOs.Login
{
    public sealed record RegistrationResponseDto(
        bool IsSuccessful,
        string? Error
        );

}
