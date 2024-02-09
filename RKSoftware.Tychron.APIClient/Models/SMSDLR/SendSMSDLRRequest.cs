using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models.SmsDlr;

/// <summary>
/// Request SMS DLR via HTTP
/// </summary>
public class SendSmsDlrRequest
{
    /// <summary>
    /// The ID used to identify the message.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Must be set to "sms_dlr".
    /// </summary>
    [JsonPropertyName("type")]
    public const string Type = "sms_dlr";

    /// <summary>
    /// The sending number that will appear in the DLR.
    /// The number must be formatted in E164 format, e.g. (+12003004000).
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; init; }

    /// <summary>
    /// Can be one of the following values:  
    /// </summary>
    /// <remarks>
    /// <li>
    /// <i><br/>delivered</i>
    /// <i><br/>expired</i>
    /// <i><br/>deleted</i>
    /// <i><br/>undelivered</i>
    /// <i><br/>accepted</i>
    /// <i><br/>unknown</i>
    /// <i><br/>rejected</i>
    /// </li>
    /// </remarks>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; init; }

    /// <summary>
    /// Normally a three digit code further describing an error,
    /// <br/>contact support for a full detailed list of error codes.
    /// </summary>
    [JsonPropertyName("delivery_error_code")]
    public string? DeliveryErrorCode { get; set; }

    /// <summary>
    /// The ID of the SMS message this delivery report is being created for, note that is REQUIRED.<br/>
    /// Please note for multipart messages, each segment will require its own DLR.
    /// </summary>
    [JsonPropertyName("sms_id")]
    public required string SmsId { get; init; }

    /// <summary>
    /// When sending to a valid TenDLC number the basic MCL data can be returned in the immediate request.
    /// <br/>Your account may not allow MCL on SMS requests, if you would like to have MCL return on request,
    /// <br/>or regardless of this flag being set, please contact support.
    /// </summary>
    /// <remarks>
    /// <br/>Note that MCL charges may apply if this flag is set to true (unless explicitly disabled by your account).
    ///</remarks>
    [JsonPropertyName("request_mcl")]
    public bool RequestMcl { get; set; }
}