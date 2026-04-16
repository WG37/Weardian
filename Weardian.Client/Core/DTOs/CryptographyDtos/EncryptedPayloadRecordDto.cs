using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Core.DTOs.CryptographyDtos
{
    internal sealed record EncryptedPayloadRecordDto(
        Guid EnvelopeId,
        string Name,
        string Algorithm,
        byte[] Ciphertext,
        byte[] Nonce,
        byte[] Tag,
        DateTime CreatedOn);
}
