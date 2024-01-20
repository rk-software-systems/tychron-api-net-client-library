using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Model.MMS;

public class Metadata
{
    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

    [JsonPropertyName("cc")]
    public List<string>? Cc { get; set; }

    [JsonPropertyName("recipients")]
    public List<string>? RListecipients { get; set; }
}