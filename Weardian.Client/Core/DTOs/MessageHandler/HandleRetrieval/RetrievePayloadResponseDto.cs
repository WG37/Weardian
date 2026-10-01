namespace Weardian.Client.Core.DTOs.MessageHandler.HandleRetrieval
{
    public sealed record RetrievePayloadResponseDto(
        Guid KeyId,
        string KeyName,
        string Algorithm,
        DateTime CreatedOn);
}
