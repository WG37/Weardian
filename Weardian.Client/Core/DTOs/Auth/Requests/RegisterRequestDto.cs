namespace Weardian.Client.Core.DTOs.Auth.Requests
{
    public sealed record RegisterRequestDto(
        string Email,
        string Password);
}
