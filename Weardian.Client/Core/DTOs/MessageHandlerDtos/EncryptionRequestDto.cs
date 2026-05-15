namespace Weardian.Client.Core.DTOs.MessageHandlerDtos
{
    public sealed record EncryptionRequestDto(
        string KeyName,
        string Password,
        bool CreateSynced
        );
}
