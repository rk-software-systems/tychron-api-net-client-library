using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Model.SMS
{
    public class SmsPart
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
