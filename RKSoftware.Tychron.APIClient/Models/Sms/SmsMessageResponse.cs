using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

/// <summary>
/// This object represents single SMS delivered response from Tychron API.
/// </summary>
public class SmsMessageResponse
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
    /// The recipient number, which is identical to the key.
    /// <example>
    /// <code>"12003004001"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>
    /// The created message type which may be an sms or mms message based on content adaption if available.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Priority affects the urgency of a message during rate limiting. The higher the priority, the more urgent a message will be.
    /// Priorities range from 0 to 2:
    /// <list type="bullet">
    /// <item>0 Low</item>
    /// <item>1 Normal (default)</item>
    /// <item>2 High</item>
    /// </list>
    /// </summary>
    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// A list of SMS messages that make up the complete request.
    /// </summary>
    [JsonPropertyName("parts")]
    public List<SmsPart>? Parts { get; set; }

    /// <summary>
    /// The body of the message in its complete form, which will be the same as that made in the request, unless a specific encoding was requested.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// The encoding that was used for the message.
    /// Note that messages will always return in UTF-8, the encoding is provided for informational purposes when diagnosing issues with upstream message delivery.
    /// </summary>
    [JsonPropertyName("encoding")]
    public int? Encoding { get; set; }

    /// <summary>
    /// Denotes under what conditions a delivery report be sent back for the message.
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    /// <summary>
    /// An object containing the UDH information of the request if it was split into multiple messages.
    /// </summary>
    [JsonPropertyName("udh")]
    public SmsUdh? Udh { get; set; }

    /// <summary>
    /// Denotes the time the system successfully accepted the message.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime? InsertedAt { get; set; }

    /// <summary>
    /// Denotes when the message's status was last modified.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Denotes when the message's status was last Processed.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// Denotes when the message was delivered.
    /// Will always be nil upon return from an SMS request.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("delivered_at")]
    public DateTime? DeliveredAt { get; set; }

    /// <summary>
    /// The expected time that the message will actually attempt to be sent to the ecosystem.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// Denotes the time the message expires.
    /// <example>
    /// <code>"2020-04-18T11:30:00Z"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

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
    /// This field will not be present if MCL is unavailable
    /// <example>
    /// <code>"us_acme"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    /// <summary>
    /// Country name of remote_number if available, otherwise it will be an empty string.
    /// <example>
    /// <code>"United States"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("remote_country")]
    public string? RemoteCountry { get; set; }

    /// <summary>
    /// Two-Character ISO country code of the remote number if available, otherwise it will be an empty string.
    /// <exmample>
    /// <code>"US"</code>
    /// </exmample>
    /// </summary>
    [JsonPropertyName("remote_country_code")]
    public string? RemoteCountryCode { get; set; }

    /// <summary>
    /// If the remote_number is messaging enabled, may be nil if indeterminate
    /// </summary>
    [JsonPropertyName("remote_messaging_enabled")]
    public bool? RemoteMessagingEnabled { get; set; }

    /// <summary>
    /// An object containing the Campaign information of the request, if available.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public SmsCspCampaign? CspCampaign { get; set; }
}