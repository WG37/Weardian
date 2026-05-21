using System.Text.Json;

namespace Weardian.Client.Core.Serialization
{
    public static class JsonSerializeCaseHelper
    {
        public static readonly JsonSerializerOptions CamelCaseOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static readonly JsonSerializerOptions CaseInsensitiveOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
