namespace Weardian.Client.Core.DTOs.WebViewDtos
{
    public sealed record WebViewResponseDto<T>(
        string Type,
        bool Success,
        T? Data,
        string? Error
        );
}
