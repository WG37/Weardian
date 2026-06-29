namespace Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos
{
    public sealed record RetrievePayloadResponseDto(
        Guid KeyId,
        string Name,
        string Algorithm,
        DateTime CreatedOn);
}
