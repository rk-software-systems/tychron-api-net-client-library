using System.Net.Http.Headers;
using RK.Tychron.APIClient.Model.SMS;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

namespace RK.Tychron.APIClient
{
    public class TychronSMSAPIClient
    {
        #region fields

        private readonly HttpClient _httpClient;
        private readonly TychronSettings _config;

        #endregion

        #region ctors

        public TychronSMSAPIClient(HttpClient httpClient, IOptions<TychronSettings> settings)
        {
            _httpClient = httpClient;
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));
            _config = settings.Value;

        }

        #endregion

        #region methods

        public async Task<List<SmsResponseModel?>> SendSms(SmsRequestModel request)
        {
            var response =
                await _httpClient.PostAsync(_config.BaseUrl, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8));

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var document = JsonNode.Parse(responseBody)!;

            var root = document.Root;
            var smsArray = root.AsArray();

            var result = new List<SmsResponseModel?>();

            foreach (var node in smsArray)
            {
                foreach (var phone in request.To!)
                {
                    if (node?[$"{phone}"] is { } responseNode)
                    {
                        result.Add(responseNode.Deserialize<SmsResponseModel>());
                    }
                }
            }
            return result;
        }

        #endregion
    }
}