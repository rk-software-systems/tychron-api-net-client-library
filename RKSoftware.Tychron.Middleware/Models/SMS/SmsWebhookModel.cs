using System.Text.Json.Serialization;
using RKSoftware.Tychron.Middleware.Models;
using RKSoftware.Tychron.Middleware.TextResources;
using RKSoftware.Tychron.Middleware.Error;

namespace RKSoftware.Tychron.Middleware.Model.Sms;

/// <summary>
/// Sms Webhook Model
/// </summary>
public class SmsWebhookModel : IValidationSubject
{
    /// <summary>
    /// The ID used to identify the message.
    /// <para>
    /// Example:
    /// <code> "01E7NBVFJA6GQTEEV0YAQP9EMT" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Parameter used to specify whether the message is a SMS message or SMS Delivery Receipt.
    /// <para>
    /// Example:
    /// <code> "sms" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The sending number that will appear in the message.
    /// The number must be formatted in a plain format, e.g (12003004000).
    /// <para>
    /// Example:
    /// <code> "12003004000" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient number of the message.The number must be formatted in a plain format,
    /// e.g (12003004000).
    /// <para>
    /// Example:
    /// <code> "12003004001" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>
    /// A map containing basic information on the Campaign associated with this message.
    /// </summary>
    [JsonPropertyName("csp_campaign")]
    public SmsCspCampaign? CspCampaign { get; set; }

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
    /// An array containing the ids for each part in a multipart message.
    /// This parameter may be empty if the message is not a multipart message.
    /// </summary>
    [JsonPropertyName("parts")]
    public List<SmsPart>? Parts { get; set; }

    /// <summary>
    /// The body of the message that was received, the body will always be encoded in UTF-8,
    /// it is not necessary to decode using the sms_encoding.
    /// <para>
    /// Example:
    /// <code> "Hello World" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// The original encoding of the message, this field is only provided for reference and
    /// should not be acted upon to decode the body.
    /// <para>
    /// Example:
    /// <code> 3 </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("sms_encoding")]
    public int? SmsEncoding { get; set; }

    /// <summary>
    /// The original priority of the message
    /// <para>
    /// Example:
    /// <code> 1 </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// Used to specify if a delivery receipt is required and when it should be
    /// sent based on the request.
    /// This parameter can be specified as either "no", "always", "on_success", and "on_error".
    /// <para>
    /// Example:
    /// <code> "no" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("request_delivery_report")]
    public string? RequestDeliveryReport { get; set; }

    /// <summary>
    /// A map containing the user data header (UDH) information of a message.
    /// </summary>
    [JsonPropertyName("udh")]
    public SmsUdh? Udh { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was received by the API.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime? InsertedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was last updated by the API.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was processed by the API.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was considered delivered.
    /// Delivery confirmation requires a delivery report/receipt.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("delivered_at")]
    public DateTime DeliveredAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was scheduled for delivery.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message will be considered expired.
    /// <para>
    /// Example:
    /// <code> "2020-04-18T11:30:00.000000Z" </code>
    /// </para>
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Validation Errors
    /// </summary>
    /// <returns></returns>
    public List<TychronMiddlewareValidationError> Validate()
    {
        // Validate: id, type, from, to, body, priority, sms_encoding, inserted_at,
        // updated_at,  processed_at
        var result = new List<TychronMiddlewareValidationError>();

        if (string.IsNullOrEmpty(Id))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Id), IdRequiredErrorCode, ValidationMessages.ReceiveSmsIdRequired));
        }

        if (string.IsNullOrEmpty(Type))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Type), TypeRequiredErrorCode, ValidationMessages.ReceiveSmsTypeRequired));
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(From), FromRequiredErrorCode, ValidationMessages.ReceiveSmsFromRequired));
        }

        if (string.IsNullOrEmpty(To))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(To), ToRequiredErrorCode, ValidationMessages.ReceiveSmsToRequired));
        }

        if (string.IsNullOrEmpty(Body))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Body), BodyRequiredErrorCode, ValidationMessages.ReceiveSmsBodyRequired));
        }

        if (Priority == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Priority), PriorityRequiredErrorCode, ValidationMessages.ReceiveSmsPriorityRequired));
        }

        if (SmsEncoding == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(SmsEncoding), SmsEncodingRequiredErrorCode, ValidationMessages.ReceiveSmsEncodingRequired));
        }

        if (InsertedAt == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(InsertedAt), InsertedAtRequiredErrorCode, ValidationMessages.ReceiveSmsInsertedAtRequired));
        }

        if (UpdatedAt == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(UpdatedAt), UpdatedAtRequiredErrorCode, ValidationMessages.ReceiveSmsUpdatedAtRequired));
        }

        if (ProcessedAt == null)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(ProcessedAt), ProcessedAtRequiredErrorCode, ValidationMessages.ReceiveSmsProcessedAtRequired));
        }

        return result;
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public const string IdRequiredErrorCode = "ReceiveSms_Id_Required";
    public const string TypeRequiredErrorCode = "ReceiveSms_Type_Required";
    public const string FromRequiredErrorCode = "ReceiveSms_From_Required";
    public const string ToRequiredErrorCode = "ReceiveSms_To_Required";
    public const string BodyRequiredErrorCode = "ReceiveSms_Body_Required";
    public const string PriorityRequiredErrorCode = "ReceiveSms_Priority_Required";
    public const string SmsEncodingRequiredErrorCode = "ReceiveSms_SmsEncoding_Required";
    public const string InsertedAtRequiredErrorCode = "ReceiveSms_InsertedAt_Required";
    public const string UpdatedAtRequiredErrorCode = "ReceiveSms_UpdatedAt_Required";
    public const string ProcessedAtRequiredErrorCode = "ReceiveSms_IProcessedAt_Required";

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}