using System.Text.Json.Serialization;
using RK.Tychron.APIClient.Model.SMS;

namespace RK.Tychron.APIClient.Models.SMSDLR;

/// <summary>
/// This object represents single SMS DLR delivered response from Tychron API.
/// </summary>
public class SMSDLRMessageResponseModel
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

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("encoding")]
    public int Encoding { get; set; }

    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    [JsonPropertyName("delivery_error_code")]
    public string? DeliveryErrorCode { get; set; }

    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("processed_at")]
    public DateTime ProcessedAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("sent_at")]
    public DateTime SentAt { get; set; }

    [JsonPropertyName("submitted_at")]
    public DateTime SubmittedAt { get; set; }

    [JsonPropertyName("done_at")]
    public DateTime DoneAt { get; set; }

    [JsonPropertyName("csp_campaign")]
    public SmsCspCampaign? CspCampaign { get; set; }
}