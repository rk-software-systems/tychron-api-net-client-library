using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using RK.Tychron.APIClient.Model.SMS;

namespace RK.Tychron.APIClient;

public class TychronClientMiddleware
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public TychronClientMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _ = next;
        _serviceScopeFactory = serviceScopeFactory;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(namingPolicy: null, allowIntegerValues: true)
            }
        };
    }

    public async Task InvokeSMSHandlerAsync(HttpContext context)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        //todo: using TychronSMSDLRWebhooksClient

        var webhooksClient = scope.ServiceProvider.GetRequiredService<TychronSMSDLRWebhooksClient>();

        var result = webhooksClient.ReceiveSMSDLR(new ReceiveSMSDLRRequest());

        await context.Response.WriteAsync(result.Result.ToString()).ConfigureAwait(false);
    }
}