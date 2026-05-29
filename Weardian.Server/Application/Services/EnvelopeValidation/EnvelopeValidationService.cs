using Weardian.Server.Application.DTOs.CryptographyDtos;
using Weardian.Server.Application.Interfaces;

namespace Weardian.Server.Application.Services.EnvelopeValidation
{
    public class EnvelopeValidationService : IEnvelopeValidationService
    {
        public ValidationResults ValidateEnvelope(EncryptedEnvelopeSyncRequestDto envelope)
        {
            var results = new ValidationResults();

            Rules.KeyTypeValidator.ValidateKeyType(envelope, results);

            return results;
        }
    }
}
