namespace RK.Tychron.APIClient
{
    public class TychronMMSAPIClient
    {
        private readonly HttpClient _httpClient;

        public TychronMMSAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
