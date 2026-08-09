using MyRoot.Application.Interfaces.Services.Watchables;
using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using MyRoot.Application.Services.Watchables;
using MyRoot.Application.Services.Auth;

namespace MyRoot.Application
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.RegisterServices();

            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Auth
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            // Watchable
            services.AddScoped<IWatchableService, WatchableService>();

            return services;
        }
    }
}
