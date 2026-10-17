namespace Weardian.Server.Application.DTOs.Register
{
    public sealed record RegistrationResponseDto(
        bool IsSuccessful,
        string? Error
        );

}
