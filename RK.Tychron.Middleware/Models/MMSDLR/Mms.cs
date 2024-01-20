using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Models.MMSDLR;

public class Mms
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}