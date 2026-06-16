using Weardian.Server.Domain.Enums;

namespace Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.ResponseDtos.Symmetric
{
    public sealed record PayloadRecordResponseDto(
        Guid EnvelopeId,
        string Name,
        KeyType KeyType,
        int EnvelopeVersion,
        string Algorithm,
        byte[] Nonce,
        byte[] Ciphertext,
        byte[] Tag);
}
