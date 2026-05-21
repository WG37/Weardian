namespace Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos
{
    public sealed record RetrieveKeyResponseDto(
        Guid EnvelopeId,
        string Name,
        string Algorithm,
        DateTime CreatedOn);
}
