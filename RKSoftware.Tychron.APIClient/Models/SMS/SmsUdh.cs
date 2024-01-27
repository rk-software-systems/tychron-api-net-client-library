using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.SMS
{
    /// <summary>
    /// An object containing the UDH information of the request if it was split into multiple messages.
    /// </summary>
    public class SmsUdh
    {
        /// <summary>
        /// The number assigned to the segments that is used identify them for transport.
        /// </summary>
        [JsonPropertyName("ref_num")]
        public int RefNum { get; set; }

        /// <summary>
        /// The number of segments that were created for the request.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
