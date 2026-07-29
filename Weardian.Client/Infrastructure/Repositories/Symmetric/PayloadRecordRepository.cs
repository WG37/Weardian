using System.IO;
using System.Text.Json;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Domain.PayloadRecords.Symmetric;
using Weardian.Client.Infrastructure.Native.PathBuilder;
using Weardian.Client.Infrastructure.Storage.Atomic;

namespace Weardian.Client.Infrastructure.Repositories.Symmetric
{
    public class PayloadRecordRepository : IPayloadRecordRepository
    {
        // split key & payload records
        public async Task AddLocalPayloadRecordAsync(PayloadRecord payloadRecord)
        {
            var payloadPath = AppDataPaths.BlobPath(payloadRecord.EnvelopeId);

            var jsonPayload = JsonSerializer.Serialize(payloadRecord);
            await AtomicFileWriter.WriteToFileAsync(payloadPath, jsonPayload);
        }

        public async Task<IReadOnlyList<PayloadRecord>> GetLocalPayloadRecordsAsync()
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                return [];

            var payloadFiles = Directory.EnumerateFiles(AppDataPaths.BlobsDir, "*.blob");
            var results = new List<PayloadRecord>();

            foreach (var payload in payloadFiles)
            {
                var json = await File.ReadAllTextAsync(payload);
                var payloadRecord = JsonSerializer.Deserialize<PayloadRecord>(json);

                if (payloadRecord == null)
                    continue;

                results.Add(payloadRecord);
            }
            
            return results;
        }

        public async Task<PayloadRecord> GetLocalPayloadRecordByIdAsync(Guid envelopeId)
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                throw new InvalidOperationException("No local record directory exists.");

            var payloadFile = AppDataPaths.BlobPath(envelopeId);

            if (!File.Exists(payloadFile))
                throw new FileNotFoundException($"Payload not found for the id {envelopeId}");

            var json = await File.ReadAllTextAsync(payloadFile);

            return JsonSerializer.Deserialize<PayloadRecord>(json)
                ?? throw new InvalidOperationException($"Payload file is invalid: {envelopeId}");
        }

        public bool RemoveLocalPayloadRecordById(Guid envelopeId)
        {
            if (!Directory.Exists(AppDataPaths.BlobsDir))
                throw new InvalidOperationException("No local record directory exists.");

            var payloadFile = AppDataPaths.BlobPath(envelopeId);

            if (!File.Exists(payloadFile))
                throw new FileNotFoundException($"Payload not found for the id {envelopeId}");

            File.Delete(payloadFile);

            var deleted = !File.Exists(payloadFile);
            return deleted;
        }
    }
}
