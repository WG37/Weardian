namespace Weardian.Client.Core.DTOs.MessageHandler.HandleRetrieval
{
    public sealed record RetrievePayloadResponseDto(
        Guid KeyId,
        string Name,
        string Algorithm,
        DateTime CreatedOn);
}
