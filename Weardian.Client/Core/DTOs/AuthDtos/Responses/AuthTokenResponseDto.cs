namespace Weardian.Client.Core.DTOs.AuthDtos.Responses
{
    public sealed record AuthTokenResponseDto(
        string Token,
        bool IsSuccessful,
        string? Error
        );
}
