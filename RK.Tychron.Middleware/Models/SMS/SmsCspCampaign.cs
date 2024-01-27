using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Model.SMS
{
    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    public class SmsCspCampaign
    {
        /// <summary>
        /// The Tychron issued UUID for the campaign.<br/>
        /// This will be null if no campaign is available.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// TCR issued Brand ID.
        /// </summary>
        [JsonPropertyName("tcr_brand_id")]
        public string? TcrBrandId { get; set; }

        /// <summary>
        /// TCR issued Campaign ID.
        /// </summary>
        [JsonPropertyName("tcr_campaign_id")]
        public string? TcrCampaignId { get; set; }
    }
}