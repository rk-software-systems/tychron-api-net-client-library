using RKSoftware.Tychron.Middleware.Error;
using RKSoftware.Tychron.Middleware.Models;
using RKSoftware.Tychron.Middleware.TextResources;
using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Model.Mms;

/// <summary>
/// MMS Webhook Model
/// </summary>
public class MmsWebhookModel : IValidationSubject
{
    /// <summary>
    /// The ID used to identify the message.
    /// <para>
    /// Example:
    /// <code>"01E7NBVFJA6GQTEEV0YAQP9EMT" </code>
    /// </para>  
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Denotes whether the message is an MMS Forward Request, or an MMS Delivery Report Request.
    /// <para>
    /// Example:
    /// <code>"mms_forward_req" </code>
    /// </para> 
    /// </summary>
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    /// <summary>
    /// The timestamp when the message was received by Tychron's system initially.
    /// <para>
    /// Example:
    /// <code>"2020-04-18T11:30:00.000000Z"</code>
    /// </para>
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime? InsertedAt { get; set; }

    /// <summary>
    /// The timestamp when the message was sent from Tychron's mailing system.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// The sending number that will appear in the message.
    /// The number must be formatted in a plain format, e.g. (12003004000).
    /// <para>
    /// Example:
    /// <code> "12003004000" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient numbers. The numbers will be formatted in a plain format,
    /// e.g (12003004000). The MMS API currently does not support multiple recipients,
    /// but this field is provided as a list.
    /// <para>
    /// Example:
    /// <code> ["12003004001", "12003004002"] </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("to")]
    public List<string>? To { get; set; }

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

    /// <summary>
    /// Validation model.
    /// </summary>
    /// <returns>List of errors or empty list.</returns>
    public List<TychronMiddlewareValidationError> Validate()
    {
        // Validate: id, timestamp, inserted_at, kind, from, to, metadata, data

        var result = new List<TychronMiddlewareValidationError>();

        if (string.IsNullOrEmpty(Id))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Id), IdRequiredErrorCode, ValidationMessages.ReceiveMmsIdRequired));
        }

        if (Timestamp == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Timestamp), TimestampRequiredErrorCode, ValidationMessages.ReceiveMmsTimestampRequired));
        }

        if (InsertedAt == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(InsertedAt), InsertedAtRequiredErrorCode, ValidationMessages.ReceiveMmsInsertedAtRequired));
        }

        if (string.IsNullOrEmpty(Kind))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Kind), KindRequiredErrorCode, ValidationMessages.ReceiveMmsKindRequired));
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(From), FromRequiredErrorCode, ValidationMessages.ReceiveMmsFromRequired));
        }

        if (To == null || To.Count == 0)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(To), ToRequiredErrorCode, ValidationMessages.ReceiveMmsToRequired));
        }

        if (Metadata == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Metadata), MetadataRequiredErrorCode, ValidationMessages.ReceiveMmsMetadataRequired));
        }

        if (Data == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Data), DataRequiredErrorCode, ValidationMessages.ReceiveMmsDataRequired));
        }

        return result;
    }

    /// <summary>
    /// Validation Error Code <see cref="Id"/> required.
    /// </summary>
    public const string IdRequiredErrorCode = "ReceiveMms_Id_Required";
    /// <summary>
    /// Validation Error Code <see cref="Timestamp"/> required.
    /// </summary>
    public const string TimestampRequiredErrorCode = "ReceiveMms_Timestamp_Required";
    /// <summary>
    /// Validation Error Code <see cref="InsertedAt"/> required.
    /// </summary>
    public const string InsertedAtRequiredErrorCode = "ReceiveMms_InsertedAt_Required";
    /// <summary>
    /// Validation Error Code <see cref="Kind"/> required.
    /// </summary>
    public const string KindRequiredErrorCode = "ReceiveMms_Kind_Required";
    /// <summary>
    /// Validation Error Code <see cref="From"/> required.
    /// </summary>
    public const string FromRequiredErrorCode = "ReceiveMms_From_Required";
    /// <summary>
    /// Validation Error Code <see cref="To"/> required.
    /// </summary>
    public const string ToRequiredErrorCode = "ReceiveMms_To_Required";
    /// <summary>
    /// Validation Error Code <see cref="Metadata"/> required.
    /// </summary>
    public const string MetadataRequiredErrorCode = "ReceiveMms_Metadata_Required";
    /// <summary>
    /// Validation Error Code <see cref="Data"/> required.
    /// </summary>
    public const string DataRequiredErrorCode = "ReceiveMms_Data_Required";
}