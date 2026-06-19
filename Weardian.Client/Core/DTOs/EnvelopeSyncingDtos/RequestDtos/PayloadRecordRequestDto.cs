using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.EnvelopeSyncingDtos.RequestDtos
{
    public sealed record PayloadRecordRequestDto(
        Guid EnvelopeId,
        string Name,
        KeyType KeyType,
        int EnvelopeVersion,
        string Algorithm,
        byte[] Nonce,
        byte[] Ciphertext,
        byte[] Tag);
}
