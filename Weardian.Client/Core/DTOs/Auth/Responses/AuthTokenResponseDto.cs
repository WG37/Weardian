namespace Weardian.Client.Core.DTOs.Auth.Responses
{
    public sealed record AuthTokenResponseDto(
        string Token,
        bool IsSuccessful,
        string? Error
        );
}
