namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record UpdateUserPasswordRequestDto(
        string UserId,
        string CurrentPassword,
        string NewPassword);
}
