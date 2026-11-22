namespace Weardian.Server.Application.DTOs.Account
{
    public sealed record UpdateUserPasswordRequestDto(
        string OldPassword,
        string NewPassword);
}
