namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record UserAccountResponseDto(
        bool IsSuccessful,
        string? Error);
}
