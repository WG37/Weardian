using Weardian.Server.Application.DTOs.CryptographyDtos.EncryptedEnvelopes.RequestDtos.Symmetric;

namespace Weardian.Server.Application.Services.EnvelopeValidation.Rules
{
    public static class KeyTypeValidator
    {
        public static ValidationResults ValidateKeyType(EncryptedEnvelopeSyncRequestDto envelope, ValidationResults results)
        {
            if (
                envelope.KeyRequestDto.KeyType != Domain.Enums.KeyType.Encryption &&
                envelope.KeyRequestDto.KeyType != Domain.Enums.KeyType.Verification &&
                envelope.KeyRequestDto.KeyType != Domain.Enums.KeyType.Signing)
            {
                results.Errors.Add("Invalid KeyType property for key record");
            }


            if (
                envelope.PayloadRequestDto.KeyType != Domain.Enums.KeyType.Encryption &&
                envelope.PayloadRequestDto.KeyType != Domain.Enums.KeyType.Verification &&
                envelope.PayloadRequestDto.KeyType != Domain.Enums.KeyType.Signing)
            {
                results.Errors.Add("Invalid KeyType property for payload record");
            }

            return results;
        }
    }
}
