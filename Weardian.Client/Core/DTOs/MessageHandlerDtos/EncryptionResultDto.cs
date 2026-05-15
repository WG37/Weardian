using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.MessageHandlerDtos
{
    public sealed record EncryptionResultDto(
        Guid EnvelopeId,
        string KeyName,
        string Algorithm,
        KeyType KeyType
        );
}
