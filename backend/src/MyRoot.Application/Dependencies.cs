using Microsoft.Extensions.DependencyInjection;
using MyRoot.Application.Clients.Games;
using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Interfaces.Services.Auth;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Services.Auth;
using MyRoot.Application.Services.Games;

namespace MyRoot.Application
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.RegisterServices();
            services.RegisterClients();

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

        private static IServiceCollection RegisterClients(this IServiceCollection services)
        {
            services.AddScoped<ISteamClient, SteamClient>();
            services.AddScoped<IPlayStationClient, PlayStationClient>();
            services.AddScoped<IXboxClient, XboxClient>();

            return services;
        }
    }
}
