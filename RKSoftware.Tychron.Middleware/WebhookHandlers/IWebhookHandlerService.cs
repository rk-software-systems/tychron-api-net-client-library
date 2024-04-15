using System.Text.Json;

namespace RKSoftware.Tychron.Middleware.WebhookHandlers;

/// <summary>
/// Service to handle incoming webhooks
/// </summary>
public interface IWebhookHandlerService
{
    /// <summary>
    /// Handle incoming webhooks
    /// </summary>
    /// <param name="json">json document</param>
    /// <returns></returns>
    Task Handle(JsonDocument json);
}
