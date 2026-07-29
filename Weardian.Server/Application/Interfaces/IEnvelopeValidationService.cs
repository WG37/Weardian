using Weardian.Server.Application.DTOs.Cryptography.EncryptedEnvelopes.Request.Symmetric;
using Weardian.Server.Application.Services.EnvelopeValidation;

namespace Weardian.Server.Application.Interfaces
{
    public interface IEnvelopeValidationService
    {
        public ValidationResults ValidateEnvelope(EncryptedEnvelopeSyncRequestDto envelope);
    }
}
