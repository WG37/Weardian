using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric
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
