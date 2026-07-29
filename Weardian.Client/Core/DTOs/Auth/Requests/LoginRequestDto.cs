namespace Weardian.Client.Core.DTOs.Auth.Requests
{
    public sealed record LoginRequestDto(
        string Email,
        string Password);
}
