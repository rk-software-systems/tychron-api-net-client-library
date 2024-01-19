using RK.Tychron.APIClient.Models;

namespace RK.Tychron.APIClient.Webhook
{
    /// <summary>
    /// This interface is used to handle webhook requests
    /// Implement this interface and register in Middleware Creation to be able to handle webhook requests
    /// </summary>
    /// <typeparam name="T">Type of webhook request model to be handled.</typeparam>
    public interface IWebhookHandler<T>
        where T: IValidationSubject
    {
        /// <summary>
        /// Handle incoming webhook request
        /// </summary>
        /// <param name="requestModel">Webhook request model</param>
        Task Handle(T requestModel);
    }
}
