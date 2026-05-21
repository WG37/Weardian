using Weardian.Server.Application.DTOs.ResponseDtos;

namespace Weardian.Server.Application.DTOs.CryptographyDtos
{
    public sealed record EncryptedEnvelopeResponseDto(
        KeySyncResponseDto ResponseDto);
}
