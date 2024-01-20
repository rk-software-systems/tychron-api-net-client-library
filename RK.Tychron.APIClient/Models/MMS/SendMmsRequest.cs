using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.MMS;

/// <summary>
/// Send Mms request model <br/>
/// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#request-parameters"/>
/// </summary>
public class SendMMSRequest
{
    /// <summary>
    /// The ID used to identify the message.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// An ID identifying the request transaction (this ID will be returned with the response).
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    /// <summary>
    /// Used to specify whether the message is an MMS message.<br/>
    /// The parameter for sending MMS messages is"mms_forward_request"
    /// </summary>
    [JsonPropertyName("type")]
    public const string Type = "mms_forward_request";

    /// <summary>
    /// The sender number, should be formatted in E.164 format<br/>
    /// for non-shortcode numbers, e.g. <b>(+12003004000) (65543)</b>
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; init; }

    /// <summary>
    /// A list of recipient numbers each should be formatted in E.164 format.
    /// </summary>
    [JsonPropertyName("to")]
    public required List<string> To { get; init; }

    /// <summary>
    /// Used to specify if a delivery report is required.
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public bool RequestDeliveryReport { get; set; }

    /// <summary>
    /// Used to specify if a reply report is required.
    /// </summary>
    [JsonPropertyName("request_read_reply_report")]
    public bool RequestReadReplyReport { get; set; }

    /// <summary>
    /// A string to use for the message's subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Parts list <br/>
    /// <see cref="MmsPart"/>
    /// </summary>
    [JsonPropertyName("parts")]
    public required List<MmsPart> Parts { get; set; }
}