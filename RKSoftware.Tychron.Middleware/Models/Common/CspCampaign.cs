using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models;

/// <summary>
/// A map containing basic information on the Campaign associated with this message.
/// </summary>
/// <param name="Id">
/// The Tychron issued UUID for the campaign.
/// </param>
/// <param name="TcrBrandId">
/// TCR issued Brand ID.
/// </param>
/// <param name="TcrCampaignId">
/// TCR issued Campaign ID.
/// </param>
public record class CspCampaign(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("tcr_brand_id")] string? TcrBrandId,
    [property: JsonPropertyName("tcr_campaign_id")] string? TcrCampaignId
    );