using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.SMS;

/// <summary>
/// Webhooks message response model
/// </summary>
public class ReceiveSMSDLRRequest
{
    /// <summary>
    /// The ID used to identify the DLR.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Parameter used to specify whether the message is a SMS message or SMS Delivery Receipt.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The sending number that will appear in the message.<br/>
    /// The number must be formatted in a plain format, e.g (12003004000).
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient number of the message. <br/>
    /// The number must be formatted in a plain format, e.g (12003004000)
    /// </summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>
    /// Last known processing status of the DLR message being delivered.
    /// This can be safely ignored for most use cases.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Last error code set on the DLR during processing.
    /// </summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// The delivery report status received from carriers:<br/>
    /// delivered, expired, deleted, undelivered, accepted, unknown, rejected, failed, enroute, skipped.
    /// </summary>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// The plain delivery error code normally 3 alphanumeric characters.
    /// </summary>
    [JsonPropertyName("delivery_error_code")]
    public string? DeliveryErrorCode { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was received by the API.
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was last updated by the API.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was considered delivered.
    /// </summary>
    [JsonPropertyName("done_at")]
    public DateTime DoneAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was registered as submitted to the carrier.
    /// </summary>
    [JsonPropertyName("submitted_at")]
    public DateTime SubmittedAt { get; set; }

    /// <summary>
    /// SMS
    /// </summary>
    [JsonPropertyName("sms")]
    public Sms? Sms { get; set; }
}