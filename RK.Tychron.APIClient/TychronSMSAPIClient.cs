using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient.TextResources;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RK.Tychron.APIClient
{
    public class TychronSMSAPIClient
    {
        #region constants
        private const string smsPath = "/sms";
        #endregion

        #region fields

        private readonly HttpClient _httpClient;

        #endregion

        #region ctors

        public TychronSMSAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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
        /// <exception cref="TychronAPIException">Exception that is thrown on API call error.</exception>
        /// <exception cref="TychronValidationException">
        /// Exception that is thrown on incoming model validation error.
        /// Available codes: <see cref="ToRequiredErrorCode"/>
        /// </exception>"
        public async Task<SendSMSResponse> SendSms(SendSmsRequest request)
        {
            ValidateSMSRequestModel(request);

            var response =
                await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json));

            var xcdrid = response.Headers.GetValues("X-CDRID").FirstOrDefault();

            if (response.StatusCode != HttpStatusCode.OK
                || response.StatusCode != HttpStatusCode.MultiStatus)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new TychronAPIException(xcdrid, (int)response.StatusCode, responseContent);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var document = JsonNode.Parse(responseStream, nodeOptions: new JsonNodeOptions() { PropertyNameCaseInsensitive = false })??new JsonObject();

            return new SendSMSResponse
            {
                XCDRID = xcdrid,
                Messages = GetSmsMessageResponse(document),
                PartialFailure = response.StatusCode == HttpStatusCode.MultiStatus
            };
        }
        
        #endregion

        #region helpers

        protected virtual List<SmsMessageResponseModel?> GetSmsMessageResponse(JsonNode document)
        {
            return document.AsObject()
                .Where(x => x.Value != null)
                .Select(x => JsonSerializer.Deserialize<SmsMessageResponseModel>(x.Value!.ToJsonString()))
                .ToList();
        }

        #endregion

        #region validation
        private void ValidateSMSRequestModel(SendSmsRequest request)
        {
            var errors = new List<TychronValidationError>();

            if (request.To == null || request.To.Count == 0)
            {
                // at lease one recipient is required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendSmsRequest.To),
                    ErrorCode = ToRequiredErrorCode,
                    Message = ValidationMessages.SendSMSToRequired
                });
            }

            if (string.IsNullOrEmpty(request.Body))
            {
                // Body required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendSmsRequest.Body),
                    ErrorCode = BodyRequiredErrorCode,
                    Message = ValidationMessages.SendSMSBodyRequired
                });
            }

            if (string.IsNullOrEmpty(request.From))
            {
                // Body required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendSmsRequest.From),
                    ErrorCode = FromRequiredErrorCode,
                    Message = ValidationMessages.SendSMSFromRequired
                });
            }

            if (errors.Count > 0)
            {
                throw new TychronValidationException(errors);
            }
        }

        public const string ToRequiredErrorCode = "SendSMS_To_Required";

        public const string BodyRequiredErrorCode = "SendSMS_Body_Required";

        public const string FromRequiredErrorCode = "SendSMS_From_Required";
        #endregion
    }
}