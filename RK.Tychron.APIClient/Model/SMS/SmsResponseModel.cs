using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.SMS;

/// <summary>
/// Response SMS via HTTP
/// </summary>
public class SmsResponseModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("direction")]
    public string? Direction { get; set; }

    [JsonPropertyName("from")]
    public string? From { get; set; }

    [JsonPropertyName("to")]
    public string? To { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("parts")]
    public List<Part>? Parts { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("encoding")]
    public int Encoding { get; set; }

    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    [JsonPropertyName("udh")]
    public Udh? Udh { get; set; }

    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("processed_at")]
    public DateTime ProcessedAt { get; set; }

    [JsonPropertyName("delivered_at")]
    public DateTime DeliveredAt { get; set; }

    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    [JsonPropertyName("remote_country")]
    public string? RemoteCountry { get; set; }

    [JsonPropertyName("remote_country_code")]
    public string? RemoteCountryCode { get; set; }

    [JsonPropertyName("remote_messaging_enabled")]
    public bool RemoteMessagingEnabled { get; set; }

    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }
}

public class CspCampaign
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("tcr_brand_id")]
    public string? TcrBrandId { get; set; }

    [JsonPropertyName("tcr_campaign_id")]
    public string? TcrCampaignId { get; set; }
}

public class Part
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class Udh
{
    [JsonPropertyName("ref_num")]
    public int RefNum { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}