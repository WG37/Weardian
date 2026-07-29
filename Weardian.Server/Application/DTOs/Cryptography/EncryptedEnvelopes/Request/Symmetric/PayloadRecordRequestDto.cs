using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric
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
