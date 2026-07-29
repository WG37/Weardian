namespace Weardian.Server.Application.DTOs.Login
{
    public sealed record AuthTokenResponseDto(
        string? Token,
        bool IsSuccessful,
        string? Error);
}
