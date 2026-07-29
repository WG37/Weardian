using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.MessageHandler.HandleEncryption
{
    public sealed record EncryptionResponseDto(
        Guid KeyId,
        string KeyName,
        string Algorithm,
        KeyType KeyType
        );
}
