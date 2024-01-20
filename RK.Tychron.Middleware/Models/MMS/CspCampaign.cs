using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Model.MMS;

public class CspCampaign
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("tcr_brand_id")]
    public string? TcrBrandId { get; set; }

    [JsonPropertyName("tcr_campaign_id")]
    public string? TcrCampaignId { get; set; }
}