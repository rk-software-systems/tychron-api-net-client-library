using Microsoft.AspNetCore.Http;
using System.Text;

namespace RKSoftware.Tychron.Middlewares;

/// <summary>
/// This middleware is used to authenticate requests to Tychron Webhook (Bearer Authentication).
/// </summary>
public class TychronBearerAuthMiddleware
{
    private readonly RequestDelegate _next;

    private readonly string _token;

    /// <summary>
    /// Initializes a new instance of the <see cref="TychronBearerAuthMiddleware"/> class.
    /// </summary>
    /// <param name="next">New Middleware Delegate.</param>
    /// <param name="token">Bearer Token to Authenticate requests</param>
    public TychronBearerAuthMiddleware(RequestDelegate next, string token)
    {
        _next = next;
        _token = token;
    }

    /// <summary>
    /// Execute Middleware
    /// </summary>
    /// <param name="context">Http Request Context</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
            return;
        }

        var authHeaderSplit = authHeader.ToString().Split(' ');
        if (authHeaderSplit.Length != 2 || !"Bearer".Equals(authHeaderSplit[0], StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
            return;
        }

        if (!_token.Equals(authHeaderSplit[1], StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
            return;
        }

        await _next(context);
    }
}
