using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS;

/// <summary>
/// Response Record model
/// </summary>
public class Record
{
    /// <summary>
    /// The ID supplied by the system to identify the message.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The created message type which may be an <i>"sms"</i> or <i>"mms"</i> message based on account settings.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// The ID originally supplied in the requests.
    /// </summary>
    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    /// <summary>
    /// Denotes when the message's status was last modified.
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    /// <summary>
    /// The sender number.
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; set; }

    /// <summary>
    /// The recipient number, which is identical to the key.
    /// </summary>
    [JsonPropertyName("to")]
    public required List<string> To { get; set; }

    /// <summary>
    /// Denotes under what conditions a delivery report be sent back for the message.
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public bool RequestDeliveryReport { get; set; }

    /// <summary>
    /// Denotes under what conditions a read-reply report be sent back for the message.
    /// </summary>
    [JsonPropertyName("request_read_reply_report")]
    public bool RequestReadReplyReport { get; set; }
}