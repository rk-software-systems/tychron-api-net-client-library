using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS.Webhooks;

public class Metadata
{
    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

    [JsonPropertyName("cc")]
    public List<string>? Cc { get; set; }

    [JsonPropertyName("recipients")]
    public List<string>? RListecipients { get; set; }
}