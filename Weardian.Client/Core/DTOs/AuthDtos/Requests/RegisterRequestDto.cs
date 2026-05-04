namespace Weardian.Client.Core.DTOs.AuthDtos.Requests
{
    public sealed record RegisterRequestDto(
        string Email,
        string Password);
}
