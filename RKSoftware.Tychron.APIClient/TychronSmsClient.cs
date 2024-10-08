﻿using RKSoftware.Tychron.APIClient.Errors;
using RKSoftware.Tychron.APIClient.Extensions;
using RKSoftware.Tychron.APIClient.Models.Sms;
using RKSoftware.Tychron.APIClient.Models;
using RKSoftware.Tychron.APIClient.TextResources;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RKSoftware.Tychron.APIClient;

/// <summary>
/// This is a client for Tychron SMS API.<br/>
/// Please check documentation for details: <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/"/>
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TychronSmsClient"/> class.
/// </remarks>
/// <param name="httpClient">Http Client that if going to be used to send requests to Tychron API.<br/>
/// Please follow the <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#authorization"/> documentation page to get more information about how to configure HttpClient.
/// You can also use Extension method <see cref="TychronClientsRegistrationExtensions.RegisterTychronClient{TychronSmsClient}"/> to register Tychron clients in DI container.
/// <example>
/// <code>
/// builder.Services.RegisterTychronClient{TychronSmsClient}(baseUrl, bearerKey);
/// </code>
/// </example>
/// </param>
public sealed class TychronSmsClient(HttpClient httpClient)
{
    #region constants

    private const string smsPath = "/sms";

    #endregion

    #region fields

    private readonly HttpClient _httpClient = httpClient;

    #endregion

    #region methods

    /// <summary>
    /// Send SMS messages
    /// </summary>
    /// <param name="request">Send SMS request.</param>
    /// <returns>
    /// SMS Send response.
    /// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-format"/>
    /// </returns>
    /// <exception cref="TychronApiException">Exception that is thrown on API call error.</exception>
    /// <exception cref="TychronValidationException">
    /// Exception that is thrown on incoming model validation error.
    /// Available codes: <see cref="ToRequiredErrorCode"/>
    /// </exception>"
    public async Task<BaseSmsResponse<SmsMessageResponse>> SendSms(SendSmsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ValidateSmsRequestModel(request);

        using var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(smsPath, content);

        response.Headers.TryGetValues(TychronConstants.XRequestHeaderName, out IEnumerable<string>? requestIds);
        var requestId = requestIds?.FirstOrDefault();

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            ErrorResponse? errorResponse;
            if (response.StatusCode == HttpStatusCode.MultiStatus)
            {
                
                var errorItems = responseContent.ToErrorItemDictionary()?.Select(x =>
                {
                    var item = x.Value;
                    if (x.Value == null)
                    {
                        item = new ErrorItem(null, null, null, null, null);

                    }
                    item.To = x.Key;
                    return item;
                });

                errorResponse = new ErrorResponse(errorItems != null ? new CustomList<ErrorItem>(errorItems) : null);
            }
            else
            {
                errorResponse = responseContent.ToErrorResponse();
            }
            throw new TychronApiException((int)response.StatusCode, responseContent, requestId, errorResponse);
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var document = await JsonNode.ParseAsync(responseStream, nodeOptions: new JsonNodeOptions()
        {
            PropertyNameCaseInsensitive = false
        }) ?? new JsonObject();

        return new BaseSmsResponse<SmsMessageResponse>(
            requestId, 
            new CustomList<SmsMessageResponse>(document.GetObjectsResponse<SmsMessageResponse>()),
            response.StatusCode == HttpStatusCode.MultiStatus);
    }   

    #endregion

    #region validation

    private static void ValidateSmsRequestModel(SendSmsRequest request)
    {
        var errors = new List<TychronValidationError>();

        if (request.To == null || request.To.Count == 0)
        {
            // at lease one recipient is required
            errors.Add(new TychronValidationError(nameof(SendSmsRequest.To), ToRequiredErrorCode, ValidationMessages.SendSmsToRequired));
        }

        if (string.IsNullOrEmpty(request.Body))
        {
            // Body required
            errors.Add(new TychronValidationError(nameof(SendSmsRequest.Body), BodyRequiredErrorCode, ValidationMessages.SendSmsBodyRequired));
        }

        if (string.IsNullOrEmpty(request.From))
        {
            // Body required
            errors.Add(new TychronValidationError(nameof(SendSmsRequest.From), FromRequiredErrorCode, ValidationMessages.SendSmsFromRequired));
        }

        if (errors.Count > 0)
        {
            throw new TychronValidationException(errors);
        }
    }

    #endregion

    #region error validation constants

    //Send SMS
    /// <summary>
    /// Validation error code for <see cref="SendSmsRequest.To"/> field required.
    /// </summary>
    public const string ToRequiredErrorCode = "SendSms_To_Required";

    /// <summary>
    /// Validation error code for <see cref="SendSmsRequest.Body"/> field required.
    /// </summary>
    public const string BodyRequiredErrorCode = "SendSms_Body_Required";

    /// <summary>
    /// Validation error code for <see cref="SendSmsRequest.From"/> field required.
    /// </summary>
    public const string FromRequiredErrorCode = "SendSms_From_Required";

    #endregion
}