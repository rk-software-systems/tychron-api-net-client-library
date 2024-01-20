using Microsoft.AspNetCore.Builder;
using RK.Tychron.Middleware.Models;
using RK.Tychron.Middlewares;

namespace RK.Tychron.Middleware.Extensions
{
    public static class TychronMiddlewareExtensions
    {
        public static IApplicationBuilder UseTychronMiddleware<T>(
            this IApplicationBuilder builder,
            string path,
            Action<IApplicationBuilder>? authenticationConfigurator)
            where T : IValidationSubject
        {
            return builder.MapWhen(context => context.Request.Path.Value?.StartsWith(path, StringComparison.OrdinalIgnoreCase) ?? false,
                appBuilder =>
                {
                    authenticationConfigurator?.Invoke(appBuilder);

                    appBuilder.UseMiddleware<TychronMiddleware<T>>(path);
                });
        }

        public static IApplicationBuilder UseTychronBasicAuth(
            this IApplicationBuilder builder,
            string username,
            string password)
        {
            return builder.UseMiddleware<TychronBasicAuthMiddleware>(username, password);
        }

        public static IApplicationBuilder UseTychronBearerAuth(
            this IApplicationBuilder builder,
            string token)
        {
            return builder.UseMiddleware<TychronBearerAuthMiddleware>(token);
        }
    }
}
