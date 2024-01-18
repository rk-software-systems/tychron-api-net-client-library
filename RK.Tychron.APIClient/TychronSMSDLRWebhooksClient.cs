﻿using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient.Models.SMSDLR;
using RK.Tychron.APIClient.TextResources;
using System.Net.Mime;
using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;
using RK.Tychron.APIClient.Extensions;

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
    public async Task<BaseSMSResponse<SMSDLRMessageResponseModel>> ReceivingSMSDLR(SendSMSDLRRequest request)
    {
        ValidateSmsDlrRequestModel(request);
        var response =
        await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json));

        response.Headers.TryGetValues(TychronConstants.XcdrHeaderName, out IEnumerable<string>? xcdrids);
        var xcdrid = xcdrids?.FirstOrDefault();

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new TychronAPIException(xcdrid, (int)response.StatusCode, responseContent);
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var document = JsonNode.Parse(responseStream, nodeOptions: new JsonNodeOptions()
        {
            PropertyNameCaseInsensitive = false
        }) ?? new JsonObject();

        return new BaseSMSResponse<SMSDLRMessageResponseModel>
        {
            XCDRID = xcdrid,
            Messages = document.GetObjectsResponse<SMSDLRMessageResponseModel>(),
            PartialFailure = response.StatusCode == HttpStatusCode.MultiStatus
        };
    }

    #endregion

    #region validation
    private static void ValidateSmsDlrRequestModel(SendSMSDLRRequest request)
    {
        var errors = new List<TychronValidationError>();

        if (string.IsNullOrEmpty(request.From))
        {
            // Message From is required
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(SendSMSDLRRequest.From),
                ErrorCode = FromRequiredErrorCode,
                Message = ValidationMessages.SendSMSFromDlrRequired
            });
        }

        if (string.IsNullOrEmpty(request.SmsId))
        {
            // The SmsId that is REQUIRED
            errors.Add(new TychronValidationError
            {
                FieldName = nameof(SendSMSDLRRequest.SmsId),
                ErrorCode = SmsIdRequiredErrorCode,
                Message = ValidationMessages.SendSMSSmsIdRequired
            });
        }

        if (errors.Count > 0)
        {
            throw new TychronValidationException(errors);
        }
    }
    #endregion

    #region validation constants
    //Send SMS DLR
    public const string FromRequiredErrorCode = "SendSMS_DLR_From_Required";

    public const string SmsIdRequiredErrorCode = "SendSMS_DLR_SmsId_Required";
    #endregion
}