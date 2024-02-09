using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.Sms;

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

    /// <summary>
    /// The message body that will be sent to the recipient. (Maximum of 2048 characters)
    /// <example>
    /// <code>"Hello World"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("body")]
    public required string Body { get; init; }

    /// <summary>
    /// The sending number that will appear in the message. The number must be formatted in <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#e164-format">E164 format</see>,  e.g. (+12003004000).
    /// <example>
    /// <code>"+12003004001"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; init; }

    /// <summary>
    /// The list of recipient number(s). Number(s) must be formatted in <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#e164-format">E164 format</see>, e.g. (+12003004000). A maximum of 500 numbers can be specified per request.
    /// <example>
    /// <code>["+12003004000"]</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("to")]
    public required List<string> To { get; init; }
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
    /// Used to encode messages as specified in the request. All messages are automatically encoded if this parameter is not provided. Please see the table below for the list of available <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#encodings">encodings</see>.
    /// </summary>
    [JsonPropertyName("encode_as")]
    public int? EncodeAs { get; set; }

    /// <summary>
    /// Used to specify if a delivery report is required and when it should be sent based on the request. This parameter can be specified as either:
    /// <list type="bullet">
    /// <item>no</item>
    /// <item>always</item>
    /// <item>on_success</item>
    /// <item>on_error</item>
    /// </list>
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    /// <summary>
    /// String Used to specify a duration of time that the message will be sent after the request is made. See the table on <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#duration-strings">Duration Strings</see> below for proper formatting.
    /// <example>
    /// <code>"12H 30M 10s 1000"</code>
    /// </example>
    /// </summary>
    [JsonPropertyName("scheduled_in")]
    public string? ScheduledIn { get; set; }

    /// <summary>
    /// Used to specify the exact date and time the message should be delivered. The timestamp should be formatted using the ISO 8601 standard with a UTC timezone.
    /// <example>
    /// <code>
    /// "2020-04-18T11:30:00Z"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// Used to specify a duration of time that the message will be deemed expired after the request is made. See the table on <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#duration-strings">Duration Strings</see> below for proper formatting.
    /// <example>
    /// <code>
    /// "12H 30M 10s 1000"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("expires_in")]
    public string? ExpiresIn { get; set; }

    /// <summary>
    /// Used to specify the exact date and time the message will be deemed expired. The timestamp should be formatted using the ISO 8601 standard with a UTC timezone.
    /// <example>
    /// <code>
    /// "2020-04-18T11:30:00Z"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// When sending to a valid TenDLC number the basic MCL data can be returned in the immediate request.
    /// Your account may not allow MCL on SMS requests, if you would like to have MCL return on request, or regardless of this flag being set, please contact support.
    /// Note that MCL charges may apply if this flag is set to true (unless explicitly disabled by your account).
    /// </summary>
    [JsonPropertyName("request_mcl")]
    public bool? RequestMcl { get; set; }
}