using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS.Webhooks;

public class CspCampaign
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("tcr_brand_id")]
    public string? TcrBrandId { get; set; }

    [JsonPropertyName("tcr_campaign_id")]
    public string? TcrCampaignId { get; set; }
}