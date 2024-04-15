using Microsoft.AspNetCore.Http;
using System.Text;

namespace RKSoftware.Tychron.Middlewares;

/// <summary>
/// This middleware is used to authenticate requests to Tychron Webhook (Basic Authentication).
/// </summary>
public class TychronBasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    private readonly string _username;
    private readonly string _password;

    /// <summary>
    /// Initializes a new instance of the <see cref="TychronBasicAuthMiddleware"/> class.
    /// </summary>
    /// <param name="next">Next Middleware delegate.</param>
    /// <param name="username">Basic Auth username</param>
    /// <param name="password">Basic Auth password</param>
    public TychronBasicAuthMiddleware(RequestDelegate next, string username, string password)
    {
        _next = next;
        _username = username;
        _password = password;
    }

    /// <summary>
    /// Execute Middleware
    /// </summary>
    /// <param name="context">Http request Context</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
            return;
        }

        var authHeaderSplit = authHeader.ToString().Split(' ');
        if (authHeaderSplit.Length != 2 || !"Basic".Equals(authHeaderSplit[0], StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
            return;
        }

        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderSplit[1])).Split(':', 2);
        if (credentials.Length != 2 || !_username.Equals(credentials[0], StringComparison.Ordinal)  || !_password.Equals(credentials[1], StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
            return;
        }

        await _next(context);
    }
}
