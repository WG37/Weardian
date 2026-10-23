namespace Weardian.Client.Core.DTOs.Auth.Responses
{
    public sealed record AuthTokenResponseDto(
        string Token,
        string RefreshToken,
        bool IsSuccessful,
        string? Error
        );
}
