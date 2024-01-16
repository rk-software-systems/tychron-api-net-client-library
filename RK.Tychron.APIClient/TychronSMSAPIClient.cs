using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient.TextResources;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RK.Tychron.APIClient
{
    public sealed class TychronSMSAPIClient
    {
        #region constants

        private const string smsPath = "/sms";
        private const string xcdr = "X-CDR-ID";

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
        public async Task<SendSmsResponse<SmsMessageResponseModel>> SendSms(SendSmsRequest request)
        {
            ValidateSmsRequestModel(request);

            var response =
                await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json));

            response.Headers.TryGetValues(xcdr, out IEnumerable<string>? xcdrids);
            var xcdrid = xcdrids?.FirstOrDefault();

            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.MultiStatus)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new TychronAPIException(xcdrid, (int)response.StatusCode, responseContent);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var document = JsonNode.Parse(responseStream, nodeOptions: new JsonNodeOptions()
            {
                PropertyNameCaseInsensitive = false
            }) ?? new JsonObject();

            return new SendSmsResponse<SmsMessageResponseModel>
            {
                XCDRID = xcdrid,
                Messages = GetSmsMessageResponse<SmsMessageResponseModel>(document),
                PartialFailure = response.StatusCode == HttpStatusCode.MultiStatus
            };
        }

        /// <summary>
        /// Send SMS messages
        /// </summary>
        /// <param name="request">Send SMS DLR request.</param>
        /// <returns>
        /// SMS DLR Send response.
        /// <see href="https://docs.tychron.info/sms-api/sending-sms-dlr-via-http/#request-format"/>
        /// </returns>
        /// <exception cref="TychronAPIException">Exception that is thrown on API call error.</exception>
        /// <exception cref="TychronValidationException">
        /// Exception that is thrown on incoming model validation error.
        /// Available codes: <see cref="FromDlrRequiredErrorCode"/>
        /// </exception>"
        public async Task<SendSmsResponse<SmsDlrMessageResponseModel>> SendSmsDlr(SendSmsDlrRequest request)
        {
            ValidateSmsDlrRequestModel(request);

            var response =
                await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json));

            response.Headers.TryGetValues(xcdr, out IEnumerable<string>? xcdrids);
            var xcdrid = xcdrids?.FirstOrDefault();

            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.MultiStatus)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new TychronAPIException(xcdrid, (int)response.StatusCode, responseContent);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var document = JsonNode.Parse(responseStream, nodeOptions: new JsonNodeOptions()
            {
                PropertyNameCaseInsensitive = false
            }) ?? new JsonObject();

            return new SendSmsResponse<SmsDlrMessageResponseModel>
            {
                XCDRID = xcdrid,
                Messages = GetSmsMessageResponse<SmsDlrMessageResponseModel>(document),
                PartialFailure = response.StatusCode == HttpStatusCode.MultiStatus
            };
        }

        #endregion

        #region helpers

        private static List<T> GetSmsMessageResponse<T>(JsonNode document)
        {
            return document.AsObject()
                .Where(x => x.Value != null)
                .Select(x => JsonSerializer.Deserialize<T>(x.Value!.ToJsonString())!)
                .ToList();
        }

        #endregion

        #region validation

        private static void ValidateSmsRequestModel(SendSmsRequest request)
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

        private static void ValidateSmsDlrRequestModel(SendSmsDlrRequest request)
        {
            var errors = new List<TychronValidationError>();

            if (string.IsNullOrEmpty(request.From))
            {
                // Message From is required
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendSmsDlrRequest.From),
                    ErrorCode = FromDlrRequiredErrorCode,
                    Message = ValidationMessages.SendSMSFromDlrRequired
                });
            }

            if (string.IsNullOrEmpty(request.SmsId))
            {
                // The SmsId that is REQUIRED
                errors.Add(new TychronValidationError
                {
                    FieldName = nameof(SendSmsDlrRequest.SmsId),
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

        #region error validation constants

        //Send SMS
        public const string ToRequiredErrorCode = "SendSMS_To_Required";

        public const string BodyRequiredErrorCode = "SendSMS_Body_Required";

        public const string FromRequiredErrorCode = "SendSMS_From_Required";

        //Send SMS DLR
        public const string FromDlrRequiredErrorCode = "SendSMS_Dlr_From_Required";

        public const string SmsIdRequiredErrorCode = "SendSMS_Dlr_SmsId_Required";

        #endregion
    }
}