using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS.Webhooks;

public class Data
{
    [JsonPropertyName("headers")]
    public Headers? Headers { get; set; }

    [JsonPropertyName("encoding")]
    public string? Encoding { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("is_multipart")]
    public bool IsMultipart { get; set; }

    [JsonPropertyName("parts")]
    public List<MmsPart>? Parts { get; set; }
}