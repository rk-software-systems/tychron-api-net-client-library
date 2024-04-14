using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

/// <summary>
/// An object containing the Campaign information of the request, if available.
/// </summary>
/// <param name="Id">
/// The Tychron issued UUID for the campaign.<br/>
/// This will be null if no campaign is available.
/// </param>
/// <param name="TcrBrandId">
/// TCR issued Brand ID. 
/// </param>
/// <param name="TcrCampaignId">
/// TCR issued Campaign ID.
/// </param>
public record class SmsCspCampaign(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("tcr_brand_id")] string? TcrBrandId,
    [property: JsonPropertyName("tcr_campaign_id")] string? TcrCampaignId
);