using System.Text.Json.Nodes;
using System.Text.Json;
using RK.Tychron.APIClient.Error;
using RK.Tychron.APIClient.Model.MMS;
using RK.Tychron.APIClient.Model.SMS;
using RK.Tychron.APIClient.TextResources;

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

        private static void ValidateSmsRequestModel(SendMmsRequest request)
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

            //if (string.IsNullOrEmpty(request.Body))
            //{
            //    // Body required
            //    errors.Add(new TychronValidationError
            //    {
            //        FieldName = nameof(SendSmsRequest.Body),
            //        ErrorCode = BodyRequiredErrorCode,
            //        Message = ValidationMessages.SendSMSBodyRequired
            //    });
            //}

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
