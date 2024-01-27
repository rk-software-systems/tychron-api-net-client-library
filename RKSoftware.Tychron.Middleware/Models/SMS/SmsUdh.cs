using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Model.SMS
{
    /// <summary>
    /// A map containing the user data header (UDH) information of a message.
    /// </summary>
    public class SmsUdh
    {
        /// <summary>
        /// An ID representing the entire message's reference number in Tychron's network.
        /// </summary>
        [JsonPropertyName("ref_num")]
        public int RefNum { get; set; }

        /// <summary>
        /// Denotes how many parts the entire message consists of.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
