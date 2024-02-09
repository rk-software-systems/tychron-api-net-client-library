using RKSoftware.Tychron.APIClient.Error;
using RKSoftware.Tychron.APIClient.Model.Mms;
using RKSoftware.Tychron.APIClient.TextResources;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RKSoftware.Tychron.APIClient;

/// <summary>
/// Tychron MMS client
/// </summary>
public sealed class TychronMmsClient
{
    #region constants

    private const string smsPath = "/api/v1/mms";

    #endregion

    #region fields

    private readonly HttpClient _httpClient;

    #endregion

    #region ctors

    /// <summary>
    /// Initializes a new instance of the <see cref="TychronMmsClient"/> class.
    /// </summary>
    /// <param name="httpClient">Http client that is going to be used to submit Tychron API request (please add Authorization to it.
    /// Use <see cref="Extensions.TychronClientsRegistrationExtensions.RegisterTychronClients"/> to register client with configured authentication.</param>
    public TychronMmsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion

    #region methods

    /// <summary>
    /// Send MMS messages
    /// </summary>
    /// <param name="request">Send MMS request.</param>
    /// <returns>
    /// MMS Send response.
    /// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#response-codes"/>
    /// </returns>
    /// <exception cref="TychronApiException">Exception that is thrown on API call error.</exception>
    /// <exception cref="TychronValidationException">
    /// Exception that is thrown on incoming model validation error.
    /// Available codes: <see cref="ToRequiredErrorCode"/>
    /// </exception>"
    public async Task<MmsMessageResponse?> SendMMS(SendMmsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ValidateMmsRequestModel(request);

        using var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(smsPath, content);

        response.Headers.TryGetValues(TychronConstants.XRequestHeaderName, out IEnumerable<string>? xRequestIds);
        var xRequestId = xRequestIds?.FirstOrDefault();

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new TychronApiException(xRequestId, (int)response.StatusCode, responseContent);
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var document = await JsonNode.ParseAsync(responseStream, nodeOptions: new JsonNodeOptions
        {
            PropertyNameCaseInsensitive = false
        }) ?? new JsonObject();

        var result = GetMmsMessageResponse<MmsMessageResponse>(document);
        if (result != null)
        {
            result.XRequestId = xRequestId;
        }

        return result;
    }

    #endregion

    #region helpers

    private static T? GetMmsMessageResponse<T>(JsonNode document)
    {
        return document.Deserialize<T>();
    }

    #endregion

    #region validation

    private static void ValidateMmsRequestModel(SendMmsRequest request)
    {
        var errors = new List<TychronValidationError>();

        if (request.To == null || request.To.Count == 0)
        {
            // at lease one recipient is required
            errors.Add(new TychronValidationError(nameof(SendMmsRequest.To), ToRequiredErrorCode, ValidationMessages.SendMmsToRequired));
        }

        if (string.IsNullOrEmpty(request.From))
        {
            // Body required
            errors.Add(new TychronValidationError(nameof(SendMmsRequest.From), FromRequiredErrorCode, ValidationMessages.SendSmsFromRequired));
        }

        if (request.Parts == null || request.Parts.Count == 0)
        {
            // at lease one part is required
            errors.Add(new TychronValidationError(nameof(SendMmsRequest.Parts), PartRequiredErrorCode, ValidationMessages.SendMmsPartRequired));
        }

        if (errors.Count > 0)
        {
            throw new TychronValidationException(errors);
        }
    }

    #endregion

    #region error validation constants

    //Send MMS
    /// <summary>
    /// Validation Error <see cref="SendMmsRequest.To"/> is required.
    /// </summary>
    public const string ToRequiredErrorCode = "SendMms_To_Required";

    /// <summary>
    /// Validation Error at least on <see cref="SendMmsRequest.Parts"/> is required.
    /// </summary>
    public const string PartRequiredErrorCode = "SendMms_Part_Required";
    
    /// <summary>
    /// Validation Error <see cref="SendMmsRequest.From"/> is required.
    /// </summary>
    public const string FromRequiredErrorCode = "SendMms_From_Required";

    #endregion
}