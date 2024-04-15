using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models.Sms;

/// <summary>
/// This object represents single SMS delivered response from Tychron API.
/// </summary>
/// <param name="Id">
/// The ID supplied by the system to identify the message.
/// Note in the case of multipart(i.e.UDH) messages, this ID will be the "multipart_id" that identifies all segments of the message.
/// <example>
/// <code>
/// "01EYVMVWJQ9F3VYAJ833W9GZJ9"
/// </code>
/// </example>
/// </param>
/// <param name="Direction">
/// Always "outbound"
/// </param>
/// <param name="From">
/// The sender number
/// <example>
/// <code>"12003004000"</code>
/// </example>
/// </param>
/// <param name="To">
/// The recipient number, which is identical to the key.
/// <example>
/// <code>"12003004001"</code>
/// </example>
/// </param>
/// <param name="Type">
/// The created message type which may be an sms or mms message based on content adaption if available.
/// </param>
/// <param name="Priority">
/// Priority affects the urgency of a message during rate limiting. The higher the priority, the more urgent a message will be.
/// Priorities range from 0 to 2:
/// <list type="bullet">
/// <item>0 Low</item>
/// <item>1 Normal (default)</item>
/// <item>2 High</item>
/// </list>
/// </param>
/// <param name="Parts">
/// A list of SMS messages that make up the complete request.
/// </param>
/// <param name="Body">
/// The body of the message in its complete form, which will be the same as that made in the request, unless a specific encoding was requested.
/// </param>
/// <param name="Encoding">
/// The encoding that was used for the message.
/// Note that messages will always return in UTF-8, the encoding is provided for informational purposes when diagnosing issues with upstream message delivery.
/// </param>
/// <param name="RequestDeliveryReport">Denotes under what conditions a delivery report be sent back for the message.</param>
/// <param name="Udh">An object containing the UDH information of the request if it was split into multiple messages.</param>
/// <param name="InsertedAt">
/// Denotes the time the system successfully accepted the message.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="UpdatedAt">
/// Denotes when the message's status was last modified.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="ProcessedAt">
/// Denotes when the message's status was last Processed.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="DeliveredAt">
/// Denotes when the message was delivered.
/// Will always be nil upon return from an SMS request.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="ScheduledAt">
/// The expected time that the message will actually attempt to be sent to the ecosystem.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="ExpiresAt">
/// Denotes the time the message expires.
/// <example>
/// <code>"2020-04-18T11:30:00Z"</code>
/// </example>
/// </param>
/// <param name="RemoteServiceProvider">
/// The name of the service provider.
/// This field will not be present if MCL is unavailable.
/// <example>
/// <code>"ACME Corp."</code>
/// </example>
/// </param>
/// <param name="RemoteReferenceId">
/// A Tychron issued ID which identifies the carrier the message is destined for, note that is field is only available if "return_mcl" or MCL for SMS Requests is enabled.
/// This field will not be present if MCL is unavailable
/// <example>
/// <code>"us_acme"</code>
/// </example>
/// </param>
/// <param name="RemoteCountry">
/// Country name of remote_number if available, otherwise it will be an empty string.
/// <example>
/// <code>"United States"</code>
/// </example>
/// </param>
/// <param name="RemoteCountryCode">
/// Two-Character ISO country code of the remote number if available, otherwise it will be an empty string.
/// <exmample>
/// <code>"US"</code>
/// </exmample>
/// </param>
/// <param name="RemoteMessagingEnabled">
/// If the remote_number is messaging enabled, may be nil if indeterminate
/// </param>
/// <param name="CspCampaign">
/// An object containing the Campaign information of the request, if available.
/// </param>
public record class SmsMessageResponse(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("direction")] string? Direction,
    [property: JsonPropertyName("from")] string? From,
    [property: JsonPropertyName("to")] string? To,
    [property: JsonPropertyName("type")] string? Type,
    [property: JsonPropertyName("priority")] int? Priority,
    [property: JsonPropertyName("parts")] CustomList<SmsPart>? Parts,
    [property: JsonPropertyName("body")] string? Body,
    [property: JsonPropertyName("encoding")] int? Encoding,
    [property: JsonPropertyName("request_delivery_report")] string? RequestDeliveryReport,
    [property: JsonPropertyName("udh")] SmsUdh? Udh,
    [property: JsonPropertyName("inserted_at")] DateTime? InsertedAt,
    [property: JsonPropertyName("updated_at")] DateTime? UpdatedAt,
    [property: JsonPropertyName("processed_at")] DateTime? ProcessedAt,
    [property: JsonPropertyName("delivered_at")] DateTime? DeliveredAt,
    [property: JsonPropertyName("scheduled_at")] DateTime? ScheduledAt,
    [property: JsonPropertyName("expires_at")] DateTime? ExpiresAt,
    [property: JsonPropertyName("remote_service_provider")] string? RemoteServiceProvider,
    [property: JsonPropertyName("remote_reference_id")] string? RemoteReferenceId,
    [property: JsonPropertyName("remote_country")] string? RemoteCountry,
    [property: JsonPropertyName("remote_country_code")] string? RemoteCountryCode,
    [property: JsonPropertyName("remote_messaging_enabled")] bool? RemoteMessagingEnabled,
    [property: JsonPropertyName("csp_campaign")] SmsCspCampaign? CspCampaign
    );