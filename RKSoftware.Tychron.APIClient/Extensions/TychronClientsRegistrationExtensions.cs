using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace RKSoftware.Tychron.APIClient.Extensions
{
    /// <summary>
    /// This class contains extensions that allows you to Register Tychron Clients
    /// </summary>
    public static class TychronClientsRegistrationExtensions
    {
        /// <summary>
        /// Register SMS, MMS and SMSDLR clients in DI container
        /// </summary>
        /// <param name="services">
        /// Service collection of DI container.
        ///
        /// </param>
        /// <param name="baseUrl">
        /// Base Tychron API URL
        /// <example>
        /// Default value: <code>https://sms.tychron.online/</code>
        /// </example>
        /// </param>
        /// <param name="bearerKey">Security Key that is used as Bearer Tychron API Authentication Token.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterTychronClients(this IServiceCollection services,
            Uri baseUrl,
            string bearerKey)
        {
            services.RegisterTychronClient<TychronSMSAPIClient>(baseUrl, bearerKey);
            services.RegisterTychronClient<TychronMMSAPIClient>(baseUrl, bearerKey);
            services.RegisterTychronClient<TychronSMSDLRClient>(baseUrl, bearerKey);
            return services;
        }

        private static IServiceCollection RegisterTychronClient<TClient>(this IServiceCollection services,
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
