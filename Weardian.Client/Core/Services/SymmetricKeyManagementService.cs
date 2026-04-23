using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces;
using Weardian.Client.Core.Interfaces.Cryptography;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords;

namespace Weardian.Client.Core.Services
{
    internal sealed class SymmetricKeyManagementService : ISymmetricKeyManagementService
    {
        private readonly ISymmetricCryptoService _symmetricCryptoService;
        private readonly ISymmetricKeyRepository _symmetricKeyRepo;
        public SymmetricKeyManagementService(
            ISymmetricCryptoService symmetricCryptoService, 
            ISymmetricKeyRepository symmetricKeyRepo)
        {
            _symmetricCryptoService = symmetricCryptoService;
            _symmetricKeyRepo = symmetricKeyRepo;
        }

        public async Task CreateEncryptedPasswordAsync(string keyName, string password)
        {
            if (string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("KeyName or Password input is null, empty or whitespace");

            if (keyName.Length < 3 || keyName.Length > 12)
                throw new ArgumentOutOfRangeException(nameof(keyName), 
                    "KeyName must have a minimum of 3 and max of 12 characters.");

            if (password.Length < 8)
                throw new ArgumentOutOfRangeException(nameof(password), 
                    "Password must have a length of 8 or more characters.");

            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);

            if (!hasUpper || !hasLower || !hasDigit)
                throw new ArgumentException(
                    "Password must contain at least one upper, lower and a digit.");

            SymmetricKeyRecord keyRecord;
            PayloadRecord payloadRecord;
            try
            {
                var envelope = await _symmetricCryptoService.CreateEncryptedEnvelopeAsync(password);

                keyRecord = new SymmetricKeyRecord(envelope.WrappedKey.WrappedKeyCiphertext)
                {
                    EnvelopeId = envelope.EnvelopeId,
                    EnvelopeVersion = envelope.WrappedKey.Version,
                    Name = keyName,
                    WrapAlgorithm = envelope.WrappedKey.WrapAlgorithm,
                    WrappingKeyId = envelope.WrappedKey.WrappingKeyId,
                    WrappedKeyNonce = envelope.WrappedKey.WrappedKeyNonce,
                    WrappedKeyTag = envelope.WrappedKey.WrappedKeyTag
                };

                payloadRecord = new PayloadRecord(envelope.Payload.Ciphertext)
                {
                    EnvelopeId = envelope.EnvelopeId,
                    Version = envelope.Payload.Version,
                    Name = keyName,
                    Algorithm = envelope.Payload.Algorithm,
                    Nonce = envelope.Payload.Nonce,
                    Tag = envelope.Payload.Tag
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create envelope.", ex);
            }

            try
            {
                if (keyRecord.EnvelopeId != payloadRecord.EnvelopeId)
                    throw new InvalidOperationException("EnvelopeIds do not match.");

                await _symmetricKeyRepo.AddLocalRecordsAsync(keyRecord, payloadRecord);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save KeyRecords to disk.", ex);
            }
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
            throw new NotImplementedException();
        }
    }
}
