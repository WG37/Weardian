namespace Weardian.Server.Application.DTOs.Authentication
{
    public sealed record RefreshTokenResponseDto(
        string? UserId,
        bool IsSuccessful,
        string? Error
        );
}
