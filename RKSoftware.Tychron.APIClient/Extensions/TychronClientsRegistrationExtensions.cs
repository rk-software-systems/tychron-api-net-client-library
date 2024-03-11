using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace RKSoftware.Tychron.APIClient.Extensions;

/// <summary>
/// This class contains extensions that allows you to Register Tychron Clients
/// </summary>
public static class TychronClientsRegistrationExtensions
{


    /// <summary>
    /// Register SMS, MMS or SMS DLR clients in DI container
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <param name="services">Service collection of DI container</param>
    /// <param name="baseUrl">Base Tychron API URL
    /// <example>
    /// Default value for SMS: <code>https://sms.tychron.online/</code>
    /// Default value for MMS: <code>https://mms.tychron.online/</code>
    /// </example>
    /// </param>
    /// <param name="bearerKey"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterTychronClient<TClient>(this IServiceCollection services,
        Uri baseUrl,
        string bearerKey) where TClient : class
    {
        services.AddHttpClient<TClient>((client) =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerKey);
            client.BaseAddress = baseUrl;
        });

        return services;
    }
}
