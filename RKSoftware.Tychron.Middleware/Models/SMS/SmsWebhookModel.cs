using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Sms;

/// <summary>
/// SMS Webhook Model
/// </summary>
/// <param name="Id">
/// The ID used to identify the message.
/// <para>
/// Example:
/// <code> "01E7NBVFJA6GQTEEV0YAQP9EMT" </code>
/// </para>
/// </param>
/// <param name="Type">
/// Parameter used to specify whether the message is a SMS message or SMS Delivery Receipt.
/// <para>
/// Example:
/// <code> "sms" </code>
/// </para>
/// </param>
/// <param name="From">
/// The sending number that will appear in the message.
/// The number must be formatted in a plain format, e.g (12003004000).
/// <para>
/// Example:
/// <code> "12003004000" </code>
/// </para>
/// </param>
/// <param name="To">
/// The recipient number of the message.The number must be formatted in a plain format,
/// e.g (12003004000).
/// <para>
/// Example:
/// <code> "12003004001" </code>
/// </para>
/// </param>
/// <param name="Body">
/// The body of the message that was received, the body will always be encoded in UTF-8,
/// it is not necessary to decode using the sms_encoding.
/// <para>
/// Example:
/// <code> "Hello World" </code>
/// </para>
/// </param>
/// <param name="Priority">
/// The original priority of the message
/// <para>
/// Example:
/// <code> 1 </code>
/// </para>
/// </param>
/// <param name="SmsEncoding">
/// The original encoding of the message, this field is only provided for reference and
/// should not be acted upon to decode the body.
/// <para>
/// Example:
/// <code> 3 </code>
/// </para>
/// </param>
/// <param name="InsertedAt">
/// An ISO 8601 formatted timestamp that represents when the message was received by the API.
/// <para>
/// Example:
/// <code> "2020-04-18T11:30:00.000000Z" </code>
/// </para>
/// </param>
/// <param name="Parts">
/// An array containing the ids for each part in a multipart message.
/// This parameter may be empty if the message is not a multipart message.
/// </param>
public record class SmsWebhookModel(
    [property:JsonPropertyName("id")] string? Id,
    [property:JsonPropertyName("type")] string? Type,
    [property:JsonPropertyName("from")] string? From,
    [property:JsonPropertyName("to")] string? To,
    [property:JsonPropertyName("body")] string? Body,
    [property:JsonPropertyName("priority")] int? Priority,
    [property:JsonPropertyName("sms_encoding")] int? SmsEncoding,
    [property: JsonPropertyName("inserted_at")] DateTime? InsertedAt,
    [property: JsonPropertyName("parts")] CustomList<SmsPart>? Parts
    ) 
{
    /// <summary>
    /// Used to specify if a delivery receipt is required and when it should be
    /// sent based on the request.
    /// This parameter can be specified as either "no", "always", "on_success", and "on_error".
    /// <para>
    /// Example:
    /// <code> "no" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    /// <summary>
    /// The carrier or service provider of the remote_number.
    /// <para>
    /// Example:
    /// <code> "ACME Corp." </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    /// <summary>
    /// A Tychron issued ID for grouping service providers for billing purposes.
    /// <para>
    /// Example:
    /// <code> "us_acme" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    /// <summary>
    /// A map containing the user data header (UDH) information of a message.
    /// </summary>
    [JsonPropertyName("udh")]
    public SmsUdh? Udh { get; set; }

    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was last updated by the API.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was processed by the API.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was considered delivered.
    /// Delivery confirmation requires a delivery report/receipt.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("delivered_at")]
    public DateTime? DeliveredAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was scheduled for delivery.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message will be considered expired.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}