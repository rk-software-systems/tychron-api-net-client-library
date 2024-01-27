using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Model.MMS;

/// <summary>
/// A map containing basic information on the Campaign associated with this message.
/// </summary>
public class CspCampaign
{
    /// <summary>
    /// The Tychron issued UUID for the campaign.
    /// <example>
    /// <code>"c045cf8c-0d97-4241-9f03-1a80ba1d7acb"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// TCR issued Brand ID.
    /// <example>
    /// <code>"B123456"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("tcr_brand_id")]
    public string? TcrBrandId { get; set; }

    /// <summary>
    /// TCR issued Campaign ID.
    /// <code>"C123456"</code>
    /// </summary>
    [JsonPropertyName("tcr_campaign_id")]
    public string? TcrCampaignId { get; set; }
}