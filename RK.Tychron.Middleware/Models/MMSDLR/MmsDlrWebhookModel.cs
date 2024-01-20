using RK.Tychron.Middleware.Error;
using RK.Tychron.Middleware.Model.MMS;
using RK.Tychron.Middleware.TextResources;
using System.Text.Json.Serialization;

namespace RK.Tychron.Middleware.Models.MMSDLR;

/// <summary>
/// Mms Dlr Webhook Model
/// </summary>
public class MmsDlrWebhookModel : IValidationSubject
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
    /// <code>"mms_delivery_report_req" </code>
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
    /// The number is typically formatted in a plain format, e.g. (12003004000),
    /// however MMS may also be received from the network and can be a Sender ID
    /// instead (e.g. "Rogers").
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
    /// Unlike forward requests, DLRs typically only have 1 part with no body,
    /// only headers.
    /// </summary>
    [JsonPropertyName("data")]
    public Data? Data { get; set; }

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

    /// <summary>
    /// Tychron Validation Errors
    /// </summary>
    /// <returns></returns>
    public List<TychronMiddlewareValidationError> Validate()
    {
        // Validate: id, timestamp, inserted_at, kind, from, to, status_code, metadata, data 
        var result = new List<TychronMiddlewareValidationError>();

        if (string.IsNullOrEmpty(Id))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = IdRequiredErrorCode,
                FieldName = nameof(Id),
                Message = ValidationMessages.ReceiveMmsDlrIdRequired
            });
        }

        if (Timestamp == null)
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = TimestampRequiredErrorCode,
                FieldName = nameof(Timestamp),
                Message = ValidationMessages.ReceiveMmsDlrTimestampRequired
            });
        }

        if (InsertedAt == null)
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = InsertedAtRequiredErrorCode,
                FieldName = nameof(InsertedAt),
                Message = ValidationMessages.ReceiveMmsDlrInsertedAtRequired
            });
        }

        if (string.IsNullOrEmpty(Kind))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = KindRequiredErrorCode,
                FieldName = nameof(Kind),
                Message = ValidationMessages.ReceiveMmsDlrKindRequired
            });
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = FromRequiredErrorCode,
                FieldName = nameof(From),
                Message = ValidationMessages.ReceiveMmsDlrFromRequired
            });
        }

        if (To == null || To.Count == 0)
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = ToRequiredErrorCode,
                FieldName = nameof(To),
                Message = ValidationMessages.ReceiveMmsDlrToRequired
            });
        }

        if (string.IsNullOrEmpty(StatusCode))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = StatusCodeRequiredErrorCode,
                FieldName = nameof(StatusCode),
                Message = ValidationMessages.ReceiveMmsDlrStatusCodeRequired
            });
        }

        if (Metadata == null)
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = MetadataRequiredErrorCode,
                FieldName = nameof(Metadata),
                Message = ValidationMessages.ReceiveMmsDlrMetadataRequired
            });
        }

        if (Data == null)
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = DataRequiredErrorCode,
                FieldName = nameof(Data),
                Message = ValidationMessages.ReceiveMmsDlrDataRequired
            });
        }

        return result;
    }

    public const string IdRequiredErrorCode = "ReceiveMMS_DLR_Id_Required";
    public const string TimestampRequiredErrorCode = "ReceiveMMS_DLR_Timestamp_Required";
    public const string InsertedAtRequiredErrorCode = "ReceiveMMS_DLR_InsertedAt_Required";
    public const string KindRequiredErrorCode = "ReceiveMMS_DLR_Kind_Required";
    public const string FromRequiredErrorCode = "ReceiveMMS_DLR_From_Required";
    public const string ToRequiredErrorCode = "ReceiveMMS_DLR_To_Required";
    public const string StatusCodeRequiredErrorCode = "ReceiveMMS_DLR_StatusCode_Required";
    public const string MetadataRequiredErrorCode = "ReceiveMMS_DLR_Metadata_Required";
    public const string DataRequiredErrorCode = "ReceiveMMS_DLR_Data_Required";
}