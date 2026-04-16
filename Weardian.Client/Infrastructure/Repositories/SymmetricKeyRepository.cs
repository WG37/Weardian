using System.IO;
using System.Text.Json;
using Weardian.Client.Core.DTOs.CryptographyDtos;
using Weardian.Client.Core.DTOs.KeyDtos;
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
                return [];

            var payloadFiles = Directory.EnumerateFiles(AppDataPaths.BlobsDir, "*.blob");
        }
    }
}
