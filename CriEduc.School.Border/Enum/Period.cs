using System.Text.Json.Serialization;

namespace CriEduc.School.Border.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Period
    {
        [JsonPropertyName("Morning")]
        Morning = 1,
        [JsonPropertyName("Afternoon")]
        Afternoon = 2,
        [JsonPropertyName("Evening")]
        Evening = 3
    }
}
