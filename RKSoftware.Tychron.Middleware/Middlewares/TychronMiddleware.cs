using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RKSoftware.Tychron.Middleware.Models;
using RKSoftware.Tychron.Middleware.Webhook;
using System.Net.Mime;
using System.Text.Json;

namespace RKSoftware.Tychron.Middlewares;

/// <summary>
/// This middleware is used to receive Tychron Webhook requests.
/// </summary>
/// <typeparam name="T">Type of requests that are being received.</typeparam>
public class TychronMiddleware<T>
    where T : IValidationSubject
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    /// <summary>
    /// Execute Middleware
    /// </summary>
    /// <param name="context">Http request context.</param>
    /// <param name="handler">Object that is used to Process Incoming Webhook Request.</param>
    /// <param name="logger">Logger</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, IWebhookHandler<T> handler, ILogger logger)
    {
        if (context.Request.Method != HttpMethods.Post)
        {
            // Only POST method is allowed for this webhook endpoint
            context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            await context.Response.WriteAsync("Method not allowed.");
            return;
        }

        var content = JsonSerializer.Deserialize<T>(context.Request.Body, _jsonSerializerOptions);

        if (content == null)
        {
            // Invalid request body
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                code = InvalidRequestBodyErrorCode,
                message = "Invalid request body"
            }));
            context.Response.ContentType = MediaTypeNames.Application.Json;
            return;
        }

        var validationResult = content.Validate();
        if (validationResult.Count > 0)
        {
            // Invalid incoming model
            var errorMessage = string.Join(", ", validationResult.Select(x => x.ToString()));
            logger.LogError("Invalid Incoming Model in path: {Path}. Validation Message: {Message}", context.Request.Path, errorMessage);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                code = InvalidRequestBodyErrorCode,
                message = errorMessage
            }));

            return;
        }

        await handler.Handle(content);

        context.Response.StatusCode = StatusCodes.Status204NoContent;
    }

    /// <summary>
    /// Error code that is raised when the incoming request body is invalid.
    /// </summary>
    public const string InvalidRequestBodyErrorCode = "Tychron_Middleware_Invalid_Request_Body";
}