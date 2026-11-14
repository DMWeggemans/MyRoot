using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using MyRoot.Application.Services.Games;
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ISteamService, SteamService>();
            services.AddScoped<IPlayStationService, PlayStationService>();
            services.AddScoped<IXboxService, XboxService>();

            return services;
        }
    }
}
