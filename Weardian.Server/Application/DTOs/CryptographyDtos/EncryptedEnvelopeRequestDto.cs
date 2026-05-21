using Weardian.Server.Application.DTOs.RequestDtos;
using Weardian.Server.Domain.KeyRecords;

namespace Weardian.Server.Application.DTOs.CryptographyDtos
{
    public sealed record EncryptedEnvelopeRequestDto(
        KeySyncRequestDto RequestDto);
}
