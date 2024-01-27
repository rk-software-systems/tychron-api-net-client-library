using Microsoft.AspNetCore.Builder;
using RK.Tychron.Middleware.Models;
using RK.Tychron.Middlewares;

namespace RK.Tychron.Middleware.Extensions
{
    /// <summary>
    /// This class  contains Extension methods that can be used to Register Tychron Middleware, that can be used to receive Tychron requests.
    /// </summary>
    public static class TychronMiddlewareExtensions
    {
        /// <summary>
        /// Registers Tychron Middleware that can be used to receive Tychron requests.
        /// </summary>
        /// <typeparam name="T">Type of Tychron requests to be receiverd</typeparam>
        /// <param name="builder">Application builder</param>
        /// <param name="path">Request path that need to match for the app to run this middleware
        /// <example>
        /// <code>/tychron/sms</code>
        /// </example>
        /// </param>
        /// <param name="authenticationConfigurator">Action that can be used to Configure Tychron Authorization (Basic or Bearer)</param>
        /// <returns></returns>
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

        /// <summary>
        /// Register Basic Auth for Tychron.
        /// In case you apply this middleware for the Entire application all requests will be authenticated with the provided credentials.
        /// If you want to enable it only for Tychron requests use <see cref="UseTychronMiddleware{T}(IApplicationBuilder, string, Action{IApplicationBuilder}?)"/> authenticationConfigurator parameter
        /// </summary>
        /// <param name="builder">Application Builder.</param>
        /// <param name="username">Basic Authentication username</param>
        /// <param name="password">Basic Authentication Password</param>
        /// <returns></returns>
        public static IApplicationBuilder UseTychronBasicAuth(
            this IApplicationBuilder builder,
            string username,
            string password)
        {
            return builder.UseMiddleware<TychronBasicAuthMiddleware>(username, password);
        }

        /// <summary>
        /// Register Bearer Auth for Tychron.
        /// In case you apply this middleware for the Entire application all requests will be authenticated with the provided credentials.
        /// If you want to enable it only for Tychron requests use <see cref="UseTychronMiddleware{T}(IApplicationBuilder, string, Action{IApplicationBuilder}?)"/> authenticationConfigurator parameter
        /// </summary>
        /// <param name="builder">Application Builder.</param>
        /// <param name="token">Bearer token that tychron is going to send you webhook endpoint.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseTychronBearerAuth(
            this IApplicationBuilder builder,
            string token)
        {
            return builder.UseMiddleware<TychronBearerAuthMiddleware>(token);
        }
    }
}
