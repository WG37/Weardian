using System.IO;
using System.Text.Json;
using Weardian.Client.Core.Interfaces.Symmetric.Repositories;
using Weardian.Client.Domain.KeyRecords.Symmetric;
using Weardian.Client.Infrastructure.Native.PathBuilder;
using Weardian.Client.Infrastructure.Storage.Atomic;

namespace Weardian.Client.Infrastructure.Repositories.Symmetric
{
    public class KeyRecordRepository : IKeyRecordRepository
    {
        public async Task AddLocalKeyRecordAsync(KeyRecord keyRecord)
        {
            var keyPath = AppDataPaths.KeyRecordPath(keyRecord.EnvelopeId);

            var jsonKey = JsonSerializer.Serialize(keyRecord);

            await AtomicFileWriter.WriteToFileAsync(keyPath, jsonKey);
        }

        public async Task<IReadOnlyList<KeyRecord>> GetLocalKeyRecordsAsync()
        {
            if (!Directory.Exists(AppDataPaths.KeysDir))
                return [];

            var keyRecordFiles = Directory.EnumerateFiles(AppDataPaths.KeysDir, "*.enc");
            var results = new List<KeyRecord>();

            foreach (var key in keyRecordFiles)
            {
                var json = await File.ReadAllTextAsync(key);
                var keyRecord = JsonSerializer.Deserialize<KeyRecord>(json);

                if (keyRecord == null)
                    continue;

                results.Add(keyRecord);
            }

            return results;
        }

        public async Task<KeyRecord> GetLocalKeyRecordByIdAsync(Guid envelopeId)
        {
            if (!Directory.Exists(AppDataPaths.KeysDir))
                throw new InvalidOperationException("No local key record directory exists.");

            var keyRecordFile = AppDataPaths.KeyRecordPath(envelopeId);

            if (!File.Exists(keyRecordFile))
                throw new FileNotFoundException($"No local key record file exists: {envelopeId}");

            var json = await File.ReadAllTextAsync(keyRecordFile);

            return JsonSerializer.Deserialize<KeyRecord>(json)
                ?? throw new InvalidOperationException($"Key record file is invalid: {envelopeId}");
        }

        public async Task UpdateLocalKeyRecordByIdAsync(KeyRecord keyRecord)
        {
            if (!Directory.Exists(AppDataPaths.KeysDir))
                throw new InvalidOperationException("No local key record directory exists.");

            var keyRecordFile = AppDataPaths.KeyRecordPath(keyRecord.EnvelopeId);
            
            if (!File.Exists(keyRecordFile))
                throw new FileNotFoundException($"No local key record file exists: {keyRecord.EnvelopeId}");

            var jsonKey = JsonSerializer.Serialize(keyRecord);

            await AtomicFileWriter.WriteToFileAsync(keyRecordFile, jsonKey);
        }

        public bool RemoveLocalKeyRecordById(Guid envelopeId)
        {
            if (!Directory.Exists(AppDataPaths.KeysDir))
                throw new InvalidOperationException("No local key record directory exists.");

            var keyRecordFile = AppDataPaths.KeyRecordPath(envelopeId);

            if (!File.Exists(keyRecordFile))
                throw new FileNotFoundException($"No local key record file exists: {envelopeId}");

            File.Delete(keyRecordFile);

            var deleted = !File.Exists(keyRecordFile);
            return deleted;
        }
    }
}
