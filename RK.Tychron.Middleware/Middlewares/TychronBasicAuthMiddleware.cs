using Microsoft.AspNetCore.Http;
using System.Text;

namespace RK.Tychron.Middlewares
{
    public class TychronBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string _username;
        private readonly string _password;

        public TychronBasicAuthMiddleware(RequestDelegate next, string username, string password)
        {
            _next = next;
            _username = username;
            _password = password;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Authorization header not found.");
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();
            var authHeaderSplit = authHeader.Split(' ');
            if (authHeaderSplit.Length != 2 || authHeaderSplit[0] != "Basic")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Invalid Authorization header.");
                return;
            }

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderSplit[1])).Split(':', 2);
            if (credentials.Length != 2 || credentials[0] != _username || credentials[1] != _password)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Tychron Webhook\"");
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            await _next(context);
        }
    }
}
