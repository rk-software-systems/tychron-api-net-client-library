using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace RK.Tychron.APIClient.Extensions
{
    public static class TychronClientsRegistrationExtensions
    {
        public static IServiceCollection RegisterTychronClients(this IServiceCollection services,
            Uri baseUrl,
            string bearerKey)
        {
            services.RegisterTychronClient<TychronSMSAPIClient>(baseUrl, bearerKey);
            services.RegisterTychronClient<TychronMMSAPIClient>(baseUrl, bearerKey);
            return services;
        }

        public static IServiceCollection RegisterTychronClient<TClient>(this IServiceCollection services,
            Uri baseUrl,
            string bearerKey)
            where TClient : class
        {
            services.AddHttpClient<TClient>((client) =>
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerKey);
                client.BaseAddress = baseUrl;
            });

            return services;
        }
    }
}
