using RKSoftware.Tychron.Middleware.Error;
using RKSoftware.Tychron.Middleware.Model.Sms;
using RKSoftware.Tychron.Middleware.TextResources;
using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.Middleware.Models.SmsDlr;

/// <summary>
/// SMS DLR Webhook model
/// </summary>
public class SmsDlrWebhookModel : IValidationSubject
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
    public DateTime? InsertedAt { get; set; }

    /// <summary>
    /// An ISO 8601 formatted timestamp that represents when the message was last updated by the API.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

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
            result.Add(new TychronMiddlewareValidationError(nameof(Id), IdRequiredErrorCode, ValidationMessages.ReceiveSmsDlrIdRequired));
        }

        if (string.IsNullOrEmpty(Type))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Type), TypeRequiredErrorCode, ValidationMessages.ReceiveSmsDlrTypeRequired));
        }

        if (string.IsNullOrEmpty(From))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(From), FromRequiredErrorCode, ValidationMessages.ReceiveSmsDlrFromRequired));
        }

        if (string.IsNullOrEmpty(To))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(To), ToRequiredErrorCode, ValidationMessages.ReceiveSmsDlrToRequired ));
        }

        if (string.IsNullOrEmpty(Status))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(Status), StatusRequiredErrorCode, ValidationMessages.ReceiveSmsDlrStatusRequired));
        }

        if (string.IsNullOrEmpty(ErrorCode))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(ErrorCode), ErrorCodeRequiredErrorCode, ValidationMessages.ReceiveSmsDlrErrorCodeRequired));
        }

        if (string.IsNullOrEmpty(DeliveryStatus))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(DeliveryStatus), DeliveryStatusRequiredErrorCode, ValidationMessages.ReceiveSmsDlrDeliveryStatusRequired));
        }

        if (string.IsNullOrEmpty(DeliveryErrorCode))
        {
            result.Add(new TychronMiddlewareValidationError(nameof(DeliveryErrorCode), DeliveryErrorCodeRequiredErrorCode, ValidationMessages.ReceiveSmsDlrDeliveryErrorCodeRequired));
        }

        if (!InsertedAt.HasValue)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(InsertedAt), InsertedAtRequiredErrorCode, ValidationMessages.ReceiveSmsDlrInsertedAtRequired));
        }

        if (!UpdatedAt.HasValue)
        {
            result.Add(new TychronMiddlewareValidationError(nameof(UpdatedAt), UpdatedAtRequiredErrorCode, ValidationMessages.ReceiveSmsDlrUpdatedAtRequired));
        }

        return result;
    }

    //Receive SMS DLR
    /// <summary>
    /// Validation Error <see cref="Id"/> is required.
    /// </summary>
    public const string IdRequiredErrorCode = "ReceiveSms_Dlr_Id_Required";

    /// <summary>
    /// Validation Error <see cref="Type"/> is required.
    /// </summary>
    public const string TypeRequiredErrorCode = "ReceiveSms_Dlr_Type_Required";

    /// <summary>
    /// Validation Error <see cref="From"/> is required.
    /// </summary>
    public const string FromRequiredErrorCode = "ReceiveSms_Dlr_From_Required";

    /// <summary>
    /// Validation Error <see cref="To"/> is required.
    /// </summary>
    public const string ToRequiredErrorCode = "ReceiveSms_Dlr_To_Required";

    /// <summary>
    /// Validation Error <see cref="Status"/> is required.
    /// </summary>
    public const string StatusRequiredErrorCode = "ReceiveSms_Dlr_Status_Required";

    /// <summary>
    /// Validation Error <see cref="ErrorCode"/> is required.
    /// </summary>
    public const string ErrorCodeRequiredErrorCode = "ReceiveSms_Dlr_ErrorCode_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryStatus"/> is required.
    /// </summary>
    public const string ErrorStatusRequiredErrorCode = "ReceiveSms_Dlr_ErrorStatus_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryStatus"/> is required.
    /// </summary>
    public const string DeliveryStatusRequiredErrorCode = "ReceiveSms_Dlr_DeliveryStatus_Required";

    /// <summary>
    /// Validation Error <see cref="DeliveryErrorCode"/> is required.
    /// </summary>
    public const string DeliveryErrorCodeRequiredErrorCode = "ReceiveSms_Dlr_DeliveryErrorCode_Required";

    /// <summary>
    /// Validation Error <see cref="InsertedAt"/> is required.
    /// </summary>
    public const string InsertedAtRequiredErrorCode = "ReceiveSms_Dlr_InsertedAt_Required";

    /// <summary>
    /// Validation Error <see cref="UpdatedAt"/> is required.
    /// </summary>
    public const string UpdatedAtRequiredErrorCode = "ReceiveSms_Dlr_UpdatedAt_Required";
}