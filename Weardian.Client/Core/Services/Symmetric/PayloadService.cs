using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.DTOs.MessageHandlerDtos.HandleRetrievalDtos;
using Weardian.Client.Core.Interfaces.Symmetric;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;

namespace Weardian.Client.Core.Services.Symmetric
{
    internal class PayloadService : IPayloadService
    {
        private readonly IPayloadRecordRepository _symmetricPayloadRepo;
        private readonly IKeyRecordRepository _symmetricKeyRepo;

        public PayloadService(
            IPayloadRecordRepository symmetricPayloadRepo,
            IKeyRecordRepository symmetricKeyRepo)
        {
            _symmetricPayloadRepo = symmetricPayloadRepo;
            _symmetricKeyRepo = symmetricKeyRepo;
        }

        public async Task<IReadOnlyList<RetrieveKeyResponseDto>> GetPayloadRecordsAsync()
        {
            var payloadRecords = await _symmetricPayloadRepo.GetLocalPayloadRecordsAsync();

            var retrievedKeys = new List<RetrieveKeyResponseDto>();

            foreach (var payload in payloadRecords)
            {
                var payloadDto = new RetrieveKeyResponseDto(
                    EnvelopeId: payload.EnvelopeId,
                    Name: payload.Name,
                    Algorithm: payload.Algorithm,
                    CreatedOn: payload.CreatedOn);

                retrievedKeys.Add(payloadDto);
            }

            return retrievedKeys;
        }

        public async Task<RetrieveKeyResponseDto> GetPayloadRecordByIdAsync(Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
                throw new ArgumentException("EnvelopeId cannot be empty", nameof(envelopeId));

            var payloadRecord = await _symmetricPayloadRepo.GetLocalPayloadRecordByIdAsync(envelopeId);

            return new RetrieveKeyResponseDto(
                EnvelopeId: payloadRecord.EnvelopeId,
                Name: payloadRecord.Name,
                Algorithm: payloadRecord.Algorithm,
                CreatedOn: payloadRecord.CreatedOn);
        }

        public bool RemoveRecordsById(Guid envelopeId)
        {
            if (envelopeId == Guid.Empty)
                throw new ArgumentException("EnvelopeId cannot be empty", nameof(envelopeId));

            var payloadDeleted = _symmetricPayloadRepo.RemoveLocalPayloadRecordById(envelopeId);
            var keyRecordDeleted = _symmetricKeyRepo.RemoveLocalKeyRecordById(envelopeId);

            if (!payloadDeleted || !keyRecordDeleted)
                throw new InvalidOperationException("Failed to delete local record pair.");

            return true;
        }
    }
}
