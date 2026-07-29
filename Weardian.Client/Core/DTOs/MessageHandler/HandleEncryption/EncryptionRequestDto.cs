namespace Weardian.Client.Core.DTOs.MessageHandler.HandleEncryption
{
    public sealed record EncryptionRequestDto(
        string KeyName,
        string Password,
        bool CreateSynced
        );
}
