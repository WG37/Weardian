namespace Weardian.Server.Application.DTOs.LoginDtos
{
    public sealed record AuthTokenResponseDto(
        string? Token,
        bool IsSuccessful,
        string? Error);
}
