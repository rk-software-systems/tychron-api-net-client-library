using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

/// <summary>
/// An object containing the Campaign information of the request, if available.
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