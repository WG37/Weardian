using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleEncryptionDtos
{
    public sealed record EncryptionResponseDto(
        Guid EnvelopeId,
        string KeyName,
        string Algorithm,
        KeyType KeyType
        );
}
