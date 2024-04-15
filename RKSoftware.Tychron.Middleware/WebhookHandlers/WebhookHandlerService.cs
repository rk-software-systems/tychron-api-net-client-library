using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RKSoftware.Tychron.Middleware.Models.Mms;
using RKSoftware.Tychron.Middleware.Models.MmsDlr;
using RKSoftware.Tychron.Middleware.Models.Sms;
using RKSoftware.Tychron.Middleware.Models.SmsDlr;

namespace RKSoftware.Tychron.Middleware.WebhookHandlers;

/// <summary>
/// Service to handle incoming webhooks
/// </summary>
public class WebhookHandlerService(IServiceProvider serviceProvider, ILogger<WebhookHandlerService> logger) : IWebhookHandlerService
{
    #region fields      
    
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<WebhookHandlerService> _logger = logger;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #endregion

    #region methods

    /// <summary>
    /// Handle incoming webhooks
    /// </summary>
    /// <param name="json">json document</param>
    /// <returns></returns>
    public async Task Handle(JsonDocument json)
    {
        ArgumentNullException.ThrowIfNull(json, nameof(json));

        var jsonStr = json.RootElement.GetRawText();

        _logInputJsonTextInformation(_logger, jsonStr, null);

        if (json.RootElement.TryGetProperty("type", out var type))
        {
            // check if it is SMS report
            if ("sms".Equals(type.GetString(), StringComparison.Ordinal))
            {
                await Handle<SmsWebhookModel>(jsonStr);
            }
            // check if it is SMS DLR report
            else if ("sms_dlr".Equals(type.GetString(), StringComparison.Ordinal))
            {
                await Handle<SmsDlrWebhookModel>(jsonStr);
            }
        }
        else if (json.RootElement.TryGetProperty("kind", out var kind))
        {
            // check if it is SMS report
            if ("mms_forward_req".Equals(kind.GetString(), StringComparison.Ordinal))
            {
                await Handle<MmsWebhookModel>(jsonStr);
            }
            // check if it is SMS DLR report
            else if ("mms_delivery_report_req".Equals(kind.GetString(), StringComparison.Ordinal))
            {
                await Handle<MmsDlrWebhookModel>(jsonStr);
            }
        }
    }

    #endregion

    #region helpers

    private async Task Handle<T>(string json)
    {
        var model = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        if (model != null)
        {
            var service = _serviceProvider.GetService<IWebhookHandler<T>>();
            if (service != null)
            {
                await service.Handle(model);                
            }
            else
            {
                _logHandlerResolvingWarning(_logger, typeof(T).Name, null);
            }
        }
        else
        {
            _logModelDeserializationWarning(_logger, typeof(T).Name, null);
        }
    }

    #endregion

    #region logging constants

    private const int InputJsonTextInformation    = 90000001;
    private const int ModelDeserializationWarning = 90000002;
    private const int HandlerResolvingWarning     = 90000003;

    #endregion

    #region logging

    private static readonly Action<ILogger, string, Exception?> _logInputJsonTextInformation = LoggerMessage.Define<string>(
       LogLevel.Information,
       InputJsonTextInformation,
       "Tychron Middleware. Input json: '{Json}'.");

    private static readonly Action<ILogger, string, Exception?> _logModelDeserializationWarning = LoggerMessage.Define<string>(
       LogLevel.Warning,
       ModelDeserializationWarning,
       "Tychron Middleware. Json was not deserialized to model: '{Model}'.");

    private static readonly Action<ILogger, string, Exception?> _logHandlerResolvingWarning = LoggerMessage.Define<string>(
       LogLevel.Warning,
       HandlerResolvingWarning,
       "Tychron Middleware. Handler was not found for model: '{Model}'.");

    #endregion    
}
