using RK.Tychron.APIClient.Model.SMS;
using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Models.SMSDLR;

/// <summary>
/// This object represents single SMS DLR delivered response from Tychron API.
/// </summary>
public class SMSDLRMessageResponseModel
{
    /// <summary>
    /// The ID supplied by the system to identify the message.
    /// Note in the case of multipart(i.e.UDH) messages, this ID will be the "multipart_id" that identifies all segments of the message.
    /// <example>
    /// <code>
    /// "01EYVMVWJQ9F3VYAJ833W9GZJ9"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Always "outbound"
    /// </summary>
    [JsonPropertyName("direction")]
    public string? Direction { get; set; }

    /// <summary>
    /// The sender number
    /// <example>
    /// <code>"12003004000"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient number, extracted from the original SMS.
    /// <example>
    /// <code>"12003004001"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>
    /// Will always be "sms_dlr" for DLR requests.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The raw generated DLR body, can be safely ignored, but can also be used for debugging.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// The encoding that was used for the message.
    /// Note that messages will always return in UTF-8, the encoding is provided for informational purposes when diagnosing issues with upstream message delivery.
    /// </summary>
    [JsonPropertyName("encoding")]
    public int Encoding { get; set; }

    /// <summary>
    /// The name of the service provider.
    /// This field will not be present if MCL is unavailable.
    /// <example>
    /// <code>"ACME Corp."</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    /// <summary>
    /// A Tychron issued ID which identifies the carrier the message is destined for, note that is field is only available if "return_mcl" or MCL for SMS Requests is enabled.
    /// This field will not be present if MCL is unavailable.
    /// <example>
    /// <code>"us_acme"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    /// <summary>
    /// Will be the same as the requested "delivery_status"
    /// <list type="bullet">
    /// <item>delivered</item>
    /// <item>expired</item>
    /// <item>deleted</item>
    /// <item>undelivered</item>
    /// <item>accepted</item>
    /// <item>unknown</item>
    /// <item>rejected</item>
    /// </list>
    /// </summary>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// Will be the same as the requested "delivery_error_code"
    /// Normally a three digit code further describing an error, contact support for a full detailed list of error codes.
    /// </summary>
    [JsonPropertyName("delivery_error_code")]
    public string? DeliveryErrorCode { get; set; }

    /// <summary>
    /// Denotes the time the system successfully accepted the message.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    /// <summary>
    /// Denotes when the message's status was last modified.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Time of processing the message.
    /// </summary>
    [JsonPropertyName("processed_at")]
    public DateTime ProcessedAt { get; set; }

    /// <summary>
    /// Denotes the time the massage expires.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Denotes when the message was sent.
    /// Will always be nil upon return from an sms request.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("sent_at")]
    public DateTime SentAt { get; set; }

    /// <summary>
    /// Denotes when the original SMS was considered accepted.
    /// Will be the current timestamp.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("submitted_at")]
    public DateTime SubmittedAt { get; set; }

    /// <summary>
    /// Denotes when the original DLR was considered accepted.
    /// Will be the current timestamp.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("done_at")]
    public DateTime DoneAt { get; set; }

    /// <summary>
    /// An object containing the Campaign information of the request, if available.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public SmsCspCampaign? CspCampaign { get; set; }
}