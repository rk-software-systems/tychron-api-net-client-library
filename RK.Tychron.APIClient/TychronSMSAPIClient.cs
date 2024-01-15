using System.Net.Http.Headers;
using RK.Tychron.APIClient.Model.SMS;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;
using System.Net.Http;

namespace RK.Tychron.APIClient
{
    public class TychronSMSAPIClient
    {
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

        public async Task<List<SmsResponseModel?>> SendSms(SmsRequestModel request)
        {
            
            var response =
                await _httpClient.PostAsync(_httpClient.BaseAddress, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8));

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