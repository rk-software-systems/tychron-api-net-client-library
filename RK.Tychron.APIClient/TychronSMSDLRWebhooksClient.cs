using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient.TextResources;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using System.Text;

namespace RK.Tychron.APIClient;

/// <summary>
/// SMS DLR via HTTP (Webhooks)
/// </summary>
public class TychronSMSDLRWebhooksClient
{
    #region constants

    private const string smsPath = "/inbound ";

    #endregion

    #region fields

    private readonly HttpClient _httpClient;

    #endregion

    #region ctors

    public TychronSMSDLRWebhooksClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion

    #region methods

    /// <summary>
    /// Receiving SMS DLR via HTTP (Webhooks)
    /// </summary>
    /// <param name="request">SMS DLR Webhooks request.</param>
    /// <returns>
    /// SMS DLR Webhooks response.
    /// <see href="https://docs.tychron.info/sms-api/receiving-sms-dlr-via-http/#request-format"/>
    /// </returns>
    /// <exception cref="TychronAPIException">Exception that is thrown on API call error.</exception>
    /// <exception cref="TychronValidationException">
    /// Exception that is thrown on incoming model validation error.
    /// Available codes: <see cref="FromRequiredErrorCode"/>
    /// </exception>"
    public async Task<HttpStatusCode> ReceiveSMSDLR(ReceiveSMSDLRRequest request)
    {
        ValidateSmsDlrRequestModel(request);

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        content.Headers.Add(TychronConstants.XMessageId, request.Id);
        content.Headers.Add(TychronConstants.XMessageFormat, TychronConstants.XMessageFormatValue);

        var response =
            await _httpClient.PostAsync(smsPath, content);

        return response.StatusCode;
    }

    #endregion

    #region helpers

    #endregion

    #region validation

    private static void ValidateSmsDlrRequestModel(ReceiveSMSDLRRequest request)
    {
        var errors = new List<TychronValidationError>();

        if (string.IsNullOrEmpty(request.Id))
        {
            // field Id is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.Id),
                ErrorCode = IdRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSIdRequired
            });
        }

        if (string.IsNullOrEmpty(request.Type))
        {
            // field Type is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.Type),
                ErrorCode = TypeRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSTypeRequired
            });
        }

        if (string.IsNullOrEmpty(request.From))
        {
            // field From is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.From),
                ErrorCode = FromRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSFromRequired
            });
        }

        if (string.IsNullOrEmpty(request.To))
        {
            // field To is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.To),
                ErrorCode = ToRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSToRequired
            });
        }

        if (string.IsNullOrEmpty(request.Status))
        {
            // field Status is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.Status),
                ErrorCode = StatusRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSSStatusRequired
            });
        }

        if (string.IsNullOrEmpty(request.ErrorCode))
        {
            // field ErrorCode is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.ErrorCode),
                ErrorCode = ErrorCodeRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSSErrorCodeRequired
            });
        }
        
        if (string.IsNullOrEmpty(request.DeliveryStatus))
        {
            // field DeliveryStatus is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.DeliveryStatus),
                ErrorCode = DeliveryStatusRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSSDeliveryStatusRequired
            });
        }

        
        if (string.IsNullOrEmpty(request.DeliveryErrorCode))
        {
            // field DeliveryStatus is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(ReceiveSMSDLRRequest.DeliveryErrorCode),
                ErrorCode = DeliveryErrorCodeRequiredErrorCode,
                Message = ValidationMessages.ReceiveSMSSDeliveryErrorCodeRequired
            });
        }




        if (errors.Count > 0)
        {
            throw new TychronValidationException(errors);
        }
    }

    #endregion

    #region validation constants

    //Receive SMS DLR
    public const string IdRequiredErrorCode = "ReceiveSMS_DLR_Id_Required";

    public const string TypeRequiredErrorCode = "ReceiveSMS_DLR_Type_Required";

    public const string FromRequiredErrorCode = "ReceiveSMS_DLR_From_Required";

    public const string ToRequiredErrorCode = "ReceiveSMS_DLR_To_Required";

    public const string StatusRequiredErrorCode = "ReceiveSMS_DLR_Status_Required";

    public const string ErrorCodeRequiredErrorCode = "ReceiveSMS_DLR_ErrorCode_Required";

    public const string ErrorStatusRequiredErrorCode = "ReceiveSMS_DLR_ErrorStatus_Required";

    public const string DeliveryStatusRequiredErrorCode = "ReceiveSMS_DLR_DeliveryStatus_Required";

    public const string DeliveryErrorCodeRequiredErrorCode = "ReceiveSMS_DLR_DeliveryErrorCode_Required";

    public const string InsertedAtRequiredErrorCode = "ReceiveSMS_DLR_InsertedAt_Required";

    public const string UpdatedAtRequiredErrorCode = "ReceiveSMS_DLR_UpdatedAt_Required";

    #endregion
}