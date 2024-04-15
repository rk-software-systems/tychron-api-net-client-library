using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.Mms;

/// <summary>
/// MMS Webhook Model
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
/// <code>"mms_forward_req" </code>
/// </para> 
/// </param>
/// <param name="From">
/// The sending number that will appear in the message.
/// The number must be formatted in a plain format, e.g. (12003004000).
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
public record class MmsWebhookModel(
    [property: JsonPropertyName("id")] string? Id,
    [property: JsonPropertyName("timestamp")] DateTime? Timestamp,
    [property: JsonPropertyName("inserted_at")] DateTime? InsertedAt,
    [property: JsonPropertyName("kind")] string? Kind,
    [property: JsonPropertyName("from")] string? From,
    [property: JsonPropertyName("to")] CustomList<string>? To
    )
{
    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public CspCampaign? CspCampaign { get; set; }

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
    /// MMS has a recursive structure, in which the root Part may have more sub parts.
    /// Typically this will be a SMIL, Text and an Image, Audio, or Video part.
    /// </summary>
    [JsonPropertyName("data")]
    public MmsPart? Data { get; set; }

    /// <summary>
    /// A map containing miscellaneous information about the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }
}