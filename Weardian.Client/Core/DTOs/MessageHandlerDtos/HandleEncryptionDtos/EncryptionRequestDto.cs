namespace Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos
{
    public sealed record EncryptionRequestDto(
        string KeyName,
        string Password,
        bool CreateSynced
        );
}
