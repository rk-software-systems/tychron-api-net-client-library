using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.MmsDlr;


/// <summary>
/// MMS DLR Webhook Model
/// </summary>
/// <param name="Id">
/// The ID used to identify the message.
/// <para>
/// Example:
/// <code>"01E7NBVFJA6GQTEEV0YAQP9EMT" </code>
/// </para>
/// </param>
/// <param name="Timestamp">
/// The timestamp when the message was sent from Tychron's mailing system.
/// <para>
/// Example:
/// <code> "2020-04-18T11:30:00.000000Z" </code>
/// </para>
/// </param>
/// <param name="InsertedAt">
/// The timestamp when the message was received by Tychron's system initially.
/// <para>
/// Example:
/// <code>"2020-04-18T11:30:00.000000Z"</code>
/// </para>
/// </param>
/// <param name="Kind">
/// Denotes whether the message is an MMS Forward Request, or an MMS Delivery Report Request.
/// <para>
/// Example:
/// <code>"mms_delivery_report_req" </code>
/// </para> 
/// </param>
/// <param name="From">
/// The sending number that will appear in the message.
/// The number is typically formatted in a plain format, e.g. (12003004000),
/// however MMS may also be received from the network and can be a Sender ID
/// instead (e.g. "Rogers").
/// <para>
/// Example:
/// <code> "12003004000" </code>
/// </para>
/// </param>
/// <param name="To">
/// The recipient numbers. The numbers will be formatted in a plain format,
/// e.g (12003004000). The MMS API currently does not support multiple recipients,
/// but this field is provided as a list.
/// <para>
/// Example:
/// <code> ["12003004001", "12003004002"] </code>
/// </para>
/// </param>
public record class MmsDlrWebhookModel(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("timestamp")] DateTime? Timestamp,
    [property: JsonPropertyName("inserted_at")] DateTime? InsertedAt,
    [property: JsonPropertyName("kind")] string? Kind,
    [property: JsonPropertyName("from")] string? From,
    [property: JsonPropertyName("to")] CustomList<string>? To
    )
{
    /// <summary>
    /// The carrier or service provider of the remote_number, is typically taken from
    /// the original MMS.
    /// <para>
    /// Example:
    /// <code> "ACME Corp." </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_service_provider")]
    public string? RemoteServiceProvider { get; set; }

    /// <summary>
    ///A Tychron issued ID for grouping service providers for billing purposes.
    /// <para>
    /// Example:
    /// <code> "us_acme" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("remote_reference_id")]
    public string? RemoteReferenceId { get; set; }

    /// <summary>
    /// An MMS Status code, normalize (downcase or upcase) this value before use,
    /// as it is taken from the x-mm-status-code header directly.
    /// <para>
    /// Example:
    /// <code> "Forwarded" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("status_code")]
    public string? StatusCode { get; set; }

    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }

    /// <summary>
    /// Unlike forward requests, DLRs typically only have 1 part with no body,
    /// only headers.
    /// </summary>
    [JsonPropertyName("data")]
    public MmsPart? Data { get; set; }

    /// <summary>
    /// A map containing miscellaneous information about the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    /// <summary>
    /// A map containing only the ID of the respective MMS this DLR belongs to.
    /// </summary>
    [JsonPropertyName("mms")]
    public Mms? Mms { get; set; }
}