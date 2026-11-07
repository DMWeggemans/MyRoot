using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using MyRoot.Application.Services.Games;
using MyRoot.Application.Services.Auth;
using MyRoot.Application.Clients.Games;
using MyRoot.Application.Interfaces.Clients.Games;

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
