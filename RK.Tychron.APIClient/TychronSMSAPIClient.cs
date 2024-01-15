using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.Tychron.APIClient
{
    public class TychronSMSAPIClient
    {
        private readonly HttpClient _httpClient;

        public TychronSMSAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
