using System.IO;
using System.Text.Json;
using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.Interfaces;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Domain.PayloadRecords;
using Weardian.Client.Infrastructure.Native.PathBuilder;
using Weardian.Client.Infrastructure.Storage.Atomic;

namespace Weardian.Client.Infrastructure.Repositories
{
    internal class SymmetricKeyRepository : ISymmetricKeyRepository
    {
        public async Task AddLocalRecordsAsync(SymmetricKeyRecord keyRecord, PayloadRecord payloadRecord)
        {
            var keyPath = AppDataPaths.KeyRecordPath(keyRecord.EnvelopeId);
            var payloadPath = AppDataPaths.BlobPath(payloadRecord.EnvelopeId);

            var jsonKey = JsonSerializer.Serialize(keyRecord);
            var jsonPayload = JsonSerializer.Serialize(payloadRecord);

            await AtomicFileWriter.WriteToFileAsync(keyPath, jsonKey);
            await AtomicFileWriter.WriteToFileAsync(payloadPath, jsonPayload);
        }

        public async Task<IReadOnlyList<EncryptedPayloadRecordDto>> GetLocalPayloadRecordsAsync()
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                throw new InvalidOperationException("No local record directory exists.");

            var payloadFiles = Directory.EnumerateFiles(AppDataPaths.BlobsDir, "*.blob");
            var results = new List<EncryptedPayloadRecordDto>();

            foreach (var payload in payloadFiles)
            {
                var json = await File.ReadAllTextAsync(payload);
                var payloadRecord = JsonSerializer.Deserialize<PayloadRecord>(json);

                if (payloadRecord == null)
                    continue;

                var payloadDto = new EncryptedPayloadRecordDto(
                    EnvelopeId: payloadRecord.EnvelopeId,
                    Name: payloadRecord.Name,
                    Algorithm: payloadRecord.Algorithm,
                    Ciphertext: payloadRecord.Ciphertext.ToArray(),
                    Nonce: payloadRecord.Nonce,
                    Tag: payloadRecord.Tag,
                    CreatedOn: payloadRecord.CreatedOn);

                results.Add(payloadDto);
            }
            
            return results;
        }

        public async Task<EncryptedPayloadRecordDto> GetLocalPayloadRecordById(Guid payloadId)
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                throw new InvalidOperationException("No local record directory exists.");

            var payloadFile = AppDataPaths.BlobPath(payloadId);

            if (File.Exists(payloadFile))
                throw new FileNotFoundException($"Payload not found for the id {payloadId}");

            var json = await File.ReadAllTextAsync(payloadFile);
            var payloadRecord = JsonSerializer.Deserialize<PayloadRecord>(json);

            if (payloadRecord == null)
                throw new InvalidDataException($"Payload file is invalid: {payloadId}");

            return new EncryptedPayloadRecordDto(
                EnvelopeId: payloadRecord.EnvelopeId,
                Name: payloadRecord.Name,
                Algorithm: payloadRecord.Algorithm,
                Ciphertext: payloadRecord.Ciphertext.ToArray(),
                Nonce: payloadRecord.Nonce,
                Tag: payloadRecord.Tag,
                CreatedOn: payloadRecord.CreatedOn);
        }

        public bool RemoveLocalPayloadRecordById(Guid payloadId)
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                throw new InvalidOperationException("No local record directory exists.");

            var payloadFile = AppDataPaths.BlobPath(payloadId);

            if (!File.Exists(payloadFile))
                throw new FileNotFoundException($"Payload not found for the id {payloadId}");

            File.Delete(payloadFile);
            return true;
        }
    }
}
