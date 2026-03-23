using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weardian.Client.Infrastructure.Serialization
{
    internal static class JsonSerializerHelper
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        internal static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, Options);
        }

        internal static T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }
    }
}
