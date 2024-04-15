using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Models.Mms;

/// <summary>
/// Send MMS request model <br/>
/// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#request-parameters"/>
/// </summary>
/// <param name="From">
/// The sender number, should be formatted in E.164 format<br/>
/// for non-shortcode numbers, e.g. <b>(+12003004000) (65543)</b>
/// </param>
/// <param name="To">
/// A list of recipient numbers each should be formatted in E.164 format.
/// </param>
public record class SendMmsRequest(
    [property:JsonPropertyName("from")] string From,
    [property:JsonPropertyName("to")] CustomList<string> To
    )
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
    public string Type => "mms_forward_request";

    /// <summary>
    /// Used to specify if a delivery report is required.
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public bool? RequestDeliveryReport { get; set; }

    /// <summary>
    /// Used to specify if a reply report is required.
    /// </summary>
    [JsonPropertyName("request_read_reply_report")]
    public bool? RequestReadReplyReport { get; set; }

    /// <summary>
    /// A string to use for the message's subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// An array containing the ids for each part (each attachment) in a multipart message. This parameter may be empty if the message is not a multipart message.
    /// </summary>
    [JsonPropertyName("parts")]
    public required CustomList<MmsPart> Parts { get; set; }
}