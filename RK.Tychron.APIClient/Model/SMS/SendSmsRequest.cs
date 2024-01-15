using RK.Tychron.APIClient.Error;
using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.SMS;

/// <summary>
/// Request SMS via HTTP
/// </summary>
public class SendSmsRequest
{
    /// <summary>
    /// The ID used to identify the message.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("body")]
    public required string Body { get; set; }

    [JsonPropertyName("from")]
    public required string From { get; set; }

    [JsonPropertyName("to")]
    public required List<string> To { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("encode_as")]
    public int EncodeAs { get; set; }

    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    [JsonPropertyName("scheduled_in")]
    public string? ScheduledIn { get; set; }

    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; }

    [JsonPropertyName("expires_in")]
    public string? ExpiresIn { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("request_mcl")]
    public bool RequestMcl { get; set; }
}