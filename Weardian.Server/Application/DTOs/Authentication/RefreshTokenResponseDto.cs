namespace Weardian.Server.Application.DTOs.Authentication
{
    public sealed record RefreshTokenResponseDto(
        string? UserId,
        string? Token,
        bool IsSuccessful,
        string? Error
        );
}
