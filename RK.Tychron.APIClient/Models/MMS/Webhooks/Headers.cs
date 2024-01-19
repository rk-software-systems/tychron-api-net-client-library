using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS.Webhooks;

public class Headers
{
    [JsonPropertyName("to")]
    public string? To { get; set; }

    [JsonPropertyName("from")]
    public string? From { get; set; }

    [JsonPropertyName("cc")]
    public string? Cc { get; set; }

    [JsonPropertyName("x-mms-mm-status-code")]
    public string? XMmsMmStatusCode { get; set; }
}