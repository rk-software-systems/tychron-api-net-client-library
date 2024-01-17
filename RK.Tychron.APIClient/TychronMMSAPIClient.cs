using System.Text.Json.Nodes;
using System.Text.Json;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.MMS;
using RK.Tychron.APIClient.TextResources;
using System.Net.Mime;
using System.Net;

namespace RK.Tychron.APIClient
{
    /// <summary>
    /// Tychron MMS client
    /// </summary>
    public sealed class TychronMMSAPIClient
    {
        #region constants

        private const string smsPath = "/api/v1/mms";
        private const string xRequestHeaderName = "X-Request-ID";

        #endregion

        #region fields

        private readonly HttpClient _httpClient;

        #endregion

        #region ctors

        public TychronMMSAPIClient(HttpClient httpClient)
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
        /// <exception cref="TychronAPIException">Exception that is thrown on API call error.</exception>
        /// <exception cref="TychronValidationException">
        /// Exception that is thrown on incoming model validation error.
        /// Available codes: <see cref="ToRequiredErrorCode"/>
        /// </exception>"
        public async Task<SendMmsResponse<MmsMessageResponseModel>> SendMms(SendMmsRequest request)
        {
            ValidateMmsRequestModel(request);

            var response =
                await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json));

            response.Headers.TryGetValues(xRequestHeaderName, out IEnumerable<string>? xrequestids);
            var xrequestid = xrequestids?.FirstOrDefault();

            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.MultiStatus)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new TychronAPIException(xrequestid, (int)response.StatusCode, responseContent);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var document = JsonNode.Parse(responseStream, nodeOptions: new JsonNodeOptions()
            {
                PropertyNameCaseInsensitive = false
            }) ?? new JsonObject();

            return new SendMmsResponse<MmsMessageResponseModel>
            {
                XRequestID = xrequestid,
                Messages = GetMmsMessageResponse<MmsMessageResponseModel>(document),
                PartialFailure = response.StatusCode == HttpStatusCode.MultiStatus
            };
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
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendMmsRequest.To),
                    ErrorCode = ToRequiredErrorCode,
                    Message = ValidationMessages.SendMMSToRequired
                });
            }

            if (string.IsNullOrEmpty(request.From))
            {
                // Body required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendMmsRequest.From),
                    ErrorCode = FromRequiredErrorCode,
                    Message = ValidationMessages.SendSMSFromRequired
                });
            }

            if (request.Parts == null || request.Parts.Count == 0)
            {
                // at lease one part is required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendMmsRequest.Parts),
                    ErrorCode = PartRequiredErrorCode,
                    Message = ValidationMessages.SendMMSPartRequired
                });
            }

            if (errors.Count > 0)
            {
                throw new TychronValidationException(errors);
            }
        }

        #endregion

        #region error validation constants

        //Send MMS
        public const string ToRequiredErrorCode = "SendMMS_To_Required";

        public const string PartRequiredErrorCode = "SendMMS_Part_Required";

        public const string FromRequiredErrorCode = "SendMMS_From_Required";

        #endregion
    }
}