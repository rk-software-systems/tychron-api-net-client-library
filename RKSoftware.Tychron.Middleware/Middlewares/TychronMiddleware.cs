using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RKSoftware.Tychron.Middleware.TextResources;
using RKSoftware.Tychron.Middleware.WebhookHandlers;
using System.Net.Mime;
using System.Text.Json;

namespace RKSoftware.Tychron.Middlewares;

/// <summary>
/// This middleware is used to receive Tychron Webhook requests.
/// </summary>
public class TychronMiddleware(RequestDelegate _)
{

    /// <summary>
    /// Execute Middleware
    /// </summary>
    /// <param name="context">Http request context.</param>
    /// <param name="webhookHandlerService">Webhook handler service</param>
    /// <param name="logger">Logger</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, IWebhookHandlerService webhookHandlerService, ILogger<TychronMiddleware> logger)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        ArgumentNullException.ThrowIfNull(webhookHandlerService, nameof(webhookHandlerService));

        if (context.Request.Method != HttpMethods.Post)
        {
            // Only POST method is allowed for this webhook endpoint
            context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            return;
        }

        var jsonDocument = await JsonDocument.ParseAsync(context.Request.Body);
        if (jsonDocument == null)
        {
            // Invalid request body
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var model = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ValidationMessages.ProblemDetails_InvalidBody,
                Type = ValidationMessages.ProblemDetails_Type_400
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(model));            
            return;
        }

        await webhookHandlerService.Handle(jsonDocument);
        context.Response.StatusCode = StatusCodes.Status204NoContent;
        return;
    }
}