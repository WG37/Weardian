namespace Weardian.Server.Application.DTOs.Authentication
{
    public sealed record AuthTokenResponseDto(
        string? Token,
        string? RefreshToken,
        bool IsSuccessful,
        string? Error
        );
}
