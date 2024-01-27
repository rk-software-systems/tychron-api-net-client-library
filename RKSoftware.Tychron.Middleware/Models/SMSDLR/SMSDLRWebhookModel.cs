using RKSoftware.Tychron.Middleware.Error;
using RKSoftware.Tychron.Middleware.Model.SMS;
using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.SMSDLR;

/// <summary>
/// Webhooks message request model
/// </summary>
public class SMSDLRWebhookModel : IValidationSubject
{
    /// <summary>
    /// The ID used to identify the DLR.
    /// <example>
    /// <code>
    /// "01E7NBVFJA6GQTEEV0YAQP9EMT"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Parameter used to specify whether the message is a SMS message or SMS Delivery Receipt.
    /// <example>
    /// <code>
    /// "sms_dlr"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The sending number that will appear in the message.<br/>
    /// The number must be formatted in a plain format, e.g (12003004000).
    /// <example>
    /// <code>
    /// "12003004000"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// The recipient number of the message. <br/>
    /// The number must be formatted in a plain format, e.g (12003004000)
    /// <example>
    /// <code>
    /// "12003004001"
    /// </code>
    /// </example>
    /// </summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>
    /// Last known processing status of the DLR message being delivered.
    /// This can be safely ignored for most use cases.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Last error code set on the DLR during processing.
    /// </summary>
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// The delivery report status received from carriers:<br/>
    /// delivered, expired, deleted, undelivered, accepted, unknown, rejected, failed, enroute, skipped.
    /// </summary>
    [JsonPropertyName("delivery_status")]
    public string? DeliveryStatus { get; set; }

    /// <summary>
    /// The plain delivery error code normally 3 alphanumeric characters.
    /// </summary>
    [JsonPropertyName("delivery_error_code")]
    public string? DeliveryErrorCode { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was received by the API.
    /// </summary>
    [JsonPropertyName("inserted_at")]
    public DateTime InsertedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was last updated by the API.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was considered delivered.
    /// </summary>
    [JsonPropertyName("done_at")]
    public DateTime? DoneAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was registered as submitted to the carrier.
    /// </summary>
    [JsonPropertyName("submitted_at")]
    public DateTime? SubmittedAt { get; set; }

    /// <summary>
    /// SMS
    /// </summary>
    [JsonPropertyName("sms")]
    public Sms? Sms { get; set; }

    /// <summary>
    /// Validate if incoming request is valid.
    /// </summary>
    /// <returns>List of Validation errors or empty list in case of valid model.</returns>
    public List<TychronMiddlewareValidationError> Validate()
    {
        var result = new List<TychronMiddlewareValidationError>();
        if (string.IsNullOrEmpty(Id))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = IdRequiredErrorCode,
                FieldName = nameof(Id),
                Message = "Id is required."
            });
        }

        if (string.IsNullOrEmpty(Type))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = TypeRequiredErrorCode,
                FieldName = nameof(Type),
                Message = "Type is required."
            });
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = FromRequiredErrorCode,
                FieldName = nameof(From),
                Message = "From is required."
            });
        }

        if (string.IsNullOrEmpty(To))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = ToRequiredErrorCode,
                FieldName = nameof(To),
                Message = "To is required."
            });
        }

        if (string.IsNullOrEmpty(Status))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = StatusRequiredErrorCode,
                FieldName = nameof(Status),
                Message = "Status is required."
            });
        }

        if (string.IsNullOrEmpty(ErrorCode))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = ErrorCodeRequiredErrorCode,
                FieldName = nameof(ErrorCode),
                Message = "ErrorCode is required."
            });
        }

        if (string.IsNullOrEmpty(DeliveryStatus))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = DeliveryStatusRequiredErrorCode,
                FieldName = nameof(DeliveryStatus),
                Message = "DeliveryStatus is required."
            });
        }

        if (string.IsNullOrEmpty(DeliveryErrorCode))
        {
            result.Add(new TychronMiddlewareValidationError
            {
                ErrorCode = DeliveryErrorCodeRequiredErrorCode,
                FieldName = nameof(DeliveryErrorCode),
                Message = "DeliveryErrorCode is required."
            });
        }

        return result;
    }

    //Receive SMS DLR
    /// <summary>
    /// Validation Error <see cref="Id"/> is required.
    /// </summary>
    public const string IdRequiredErrorCode = "ReceiveSMS_DLR_Id_Required";

    /// <summary>
    /// Validation Error <see cref="Type"/> is required.
    /// </summary>
    public const string TypeRequiredErrorCode = "ReceiveSMS_DLR_Type_Required";

    /// <summary>
    /// Validation Error <see cref="From"/> is required.
    /// </summary>
    public const string FromRequiredErrorCode = "ReceiveSMS_DLR_From_Required";

    /// <summary>
    /// Validation Error <see cref="To"/> is required.
    /// </summary>
    public const string ToRequiredErrorCode = "ReceiveSMS_DLR_To_Required";

    /// <summary>
    /// Validation Error <see cref="Status"/> is required.
    /// </summary>
    public const string StatusRequiredErrorCode = "ReceiveSMS_DLR_Status_Required";

    /// <summary>
    /// Validation Error <see cref="ErrorCode"/> is required.
    /// </summary>
    public const string ErrorCodeRequiredErrorCode = "ReceiveSMS_DLR_ErrorCode_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryStatus"/> is required.
    /// </summary>
    public const string ErrorStatusRequiredErrorCode = "ReceiveSMS_DLR_ErrorStatus_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryStatus"/> is required.
    /// </summary>
    public const string DeliveryStatusRequiredErrorCode = "ReceiveSMS_DLR_DeliveryStatus_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryErrorCode"/> is required.
    /// </summary>
    public const string DeliveryErrorCodeRequiredErrorCode = "ReceiveSMS_DLR_DeliveryErrorCode_Required";
}