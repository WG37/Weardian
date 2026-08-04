using Weardian.Client.Domain.Enums;

namespace Weardian.Client.Core.DTOs.Sync.Transfers
{
    public sealed record PayloadRecordTransferDto(
        Guid EnvelopeId,
        string Name,
        KeyType KeyType,
        int EnvelopeVersion,
        string Algorithm,
        byte[] Nonce,
        byte[] Ciphertext,
        byte[] Tag);
}
