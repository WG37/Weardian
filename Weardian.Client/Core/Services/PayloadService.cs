using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces;

namespace Weardian.Client.Core.Services
{
    internal class PayloadService : IPayloadService
    {
        private readonly ISymmetricKeyRepository _symmetricKeyRepo;

        public PayloadService(ISymmetricKeyRepository symmetricKeyRepo)
        {
            _symmetricKeyRepo = symmetricKeyRepo;
        }

        public async Task<IReadOnlyList<EncryptedPayloadRecordDto>> GetPayloadRecordsAsync()
        {
            var payloadRecords = await _symmetricKeyRepo.GetLocalPayloadRecordsAsync();

            var payloadResults = new List<EncryptedPayloadRecordDto>();

            foreach (var payload in payloadRecords)
            {
                var payloadDto = new EncryptedPayloadRecordDto(
                    EnvelopeId: payload.EnvelopeId,
                    Name: payload.Name,
                    Algorithm: payload.Algorithm,
                    Ciphertext: payload.Ciphertext.ToArray(),
                    Nonce: payload.Nonce,
                    Tag: payload.Tag,
                    CreatedOn: payload.CreatedOn);

                payloadResults.Add(payloadDto);
            }

            return payloadResults;
        }

        public async Task<EncryptedPayloadRecordDto> GetPayloadRecordByIdAsync(Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
                throw new ArgumentException("EnvelopeId cannot be empty", nameof(envelopeId));

            var payloadRecord = await _symmetricKeyRepo.GetLocalPayloadRecordByIdAsync(envelopeId);

            return new EncryptedPayloadRecordDto(
                EnvelopeId: payloadRecord.EnvelopeId,
                Name: payloadRecord.Name,
                Algorithm: payloadRecord.Algorithm,
                Ciphertext: payloadRecord.Ciphertext.ToArray(),
                Nonce: payloadRecord.Nonce,
                Tag: payloadRecord.Tag,
                CreatedOn: payloadRecord.CreatedOn);
        }

        public bool RemoveRecordById(Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
                throw new ArgumentException("EnvelopeId cannot be empty", nameof(envelopeId));

            var deleted = _symmetricKeyRepo.RemoveLocalPayloadRecordById(envelopeId);

            return deleted;
        }
    }
}
