using RKSoftware.Tychron.APIClient.Error;
using RKSoftware.Tychron.APIClient.Extensions;
using RKSoftware.Tychron.APIClient.Model.SMS;
using RKSoftware.Tychron.APIClient.Models.SMSDLR;
using RKSoftware.Tychron.APIClient.TextResources;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RKSoftware.Tychron.APIClient
{
    /// <summary>
    /// This Client is used to send SMS DLR.
    /// </summary>
    public class TychronSMSDLRClient
    {
        #region constants

        private const string smsPath = "/sms";

        #endregion

        #region fields

        private readonly HttpClient _httpClient;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TychronSMSDLRClient"/> class.
        /// </summary>
        /// <param name="httpClient">Http Client that is used to Issue requests to Tychron API.</param>
        public TychronSMSDLRClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region methods
        /// <summary>
        /// Send SMS DLR
        /// </summary>
        /// <param name="request">SMS DLR request.</param>
        /// <returns>
        /// SMS DLR response.
        /// <see href="https://docs.tychron.info/sms-api/sending-sms-dlr-via-http/#request-format"/>
        /// </returns>
        /// <exception cref="TychronAPIException">Exception that is thrown on API call error.</exception>
        /// <exception cref="TychronValidationException">
        /// Exception that is thrown on incoming model validation error.
        /// Available codes: <see cref="FromRequiredErrorCode"/>
        /// </exception>"
        public async Task<BaseSMSResponse<SMSDLRMessageResponseModel>> SendSMSDLR(SendSMSDLRRequest request)
        {
            ValidateSmsDlrRequestModel(request);
            var response =
            await _httpClient.PostAsync(smsPath, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json));

            response.Headers.TryGetValues(TychronConstants.XcdrHeaderName, out IEnumerable<string>? xcdrids);
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
        /// <summary>
        /// Validation error <see cref="SendSMSDLRRequest.From"/> is required.
        /// </summary>
        public const string FromRequiredErrorCode = "SendSMS_DLR_From_Required";

        /// <summary>
        /// Validation error <see cref="SendSMSDLRRequest.SmsId"/> is required.
        /// </summary>
        public const string SmsIdRequiredErrorCode = "SendSMS_DLR_SmsId_Required";
        #endregion
    }
}