namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record UpdateUserPasswordRequestDto(
        string CurrentPassword,
        string NewPassword);
}
