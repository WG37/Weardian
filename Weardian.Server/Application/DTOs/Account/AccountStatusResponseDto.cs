namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record AccountStatusResponseDto(
        bool IsSuccessful,
        string? Error,
        DateTime? ChangedOn);
}
