using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.SMS
{
    public class SMSPart
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
