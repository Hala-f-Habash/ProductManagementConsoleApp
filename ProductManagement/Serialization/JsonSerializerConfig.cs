using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductManagement.Serialization;

public static class JsonSerializerConfig
{
    private static readonly Lazy<JsonSerializerOptions> _options =
        new(() =>
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        });

    public static JsonSerializerOptions Options => _options.Value;
}