using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RK.Tychron.APIClient.Models;
using RK.Tychron.APIClient.Webhook;
using System.Net.Mime;
using System.Text.Json;

namespace RK.Tychron.APIClient.Middlewares;

public class TychronMiddleware<T>
    where T : IValidationSubject
{
    public TychronMiddleware()
    {
    }

    public async Task InvokeAsync(HttpContext context, IWebhookHandler<T> handler, ILogger logger)
    {
        if (context.Request.Method != HttpMethods.Post)
        {
            // Only POST method is allowed for this webhook endpoint
            context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            await context.Response.WriteAsync("Method not allowed.");
            return;
        }

        var content = JsonSerializer.Deserialize<T>(context.Request.Body, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

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

    public const string InvalidRequestBodyErrorCode = "Tychron_Middleware_Invalid_Request_Body";
}