using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Models.MMSDLR.Webhooks;

public class Mms
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}