namespace Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos
{
    public sealed record RetrieveKeyResponseDto(
        Guid KeyId,
        string Name,
        string Algorithm,
        DateTime CreatedOn);
}
