namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record UserAccountStatusResponseDto(
        bool IsSuccessful,
        string? Error,
        DateTime? ChangedOn);
}
