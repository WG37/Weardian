namespace Weardian.Client.Core.DTOs.AuthDtos.Requests
{
    public sealed record LoginRequestDto(
        string Email,
        string Password);
}
