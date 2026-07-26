namespace Weardian.Server.Application.DTOs.LoginDtos
{
    public sealed record RegistrationResponseDto(
        bool IsSuccessful,
        string? Error
        );

}
