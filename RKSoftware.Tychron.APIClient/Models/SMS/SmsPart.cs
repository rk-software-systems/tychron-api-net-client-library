using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.SMS
{
    /// <summary>
    /// SMS Message part model
    /// </summary>
    public class SmsPart
    {
        /// <summary>
        /// The ID supplied by the system to identify the segment.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
