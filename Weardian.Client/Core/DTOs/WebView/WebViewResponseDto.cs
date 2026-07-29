namespace Weardian.Client.Core.DTOs.WebView
{
    public sealed record WebViewResponseDto<T>(
        string Type,
        bool Success,
        T? Data,
        string? Error
        );
}
