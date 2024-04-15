namespace RKSoftware.Tychron.Middleware.WebhookHandlers;

/// <summary>
/// This interface is used to handle webhook requests
/// Implement this interface and register in Middleware Creation to be able to handle webhook requests
/// </summary>
/// <typeparam name="T">Type of webhook request model to be handled.</typeparam>
public interface IWebhookHandler<in T>
{
    /// <summary>
    /// Handle incoming webhook request
    /// </summary>
    /// <param name="request">Webhook request model</param>
    Task Handle(T request);
}
