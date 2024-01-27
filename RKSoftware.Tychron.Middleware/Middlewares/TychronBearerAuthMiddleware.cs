using Microsoft.AspNetCore.Http;
using System.Text;

namespace RKSoftware.Tychron.Middlewares
{
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
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Authorization header not found.");
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();
            var authHeaderSplit = authHeader.Split(' ');
            if (authHeaderSplit.Length != 2 || authHeaderSplit[0] != "Bearer")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Invalid Authorization header.");
                return;
            }

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderSplit[1])).Split(':', 2);
            if (credentials.Length != 1 || credentials[0] != _token)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Bearer realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            await _next(context);
        }
    }
}
