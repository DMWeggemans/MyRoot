using Microsoft.Extensions.DependencyInjection;
using MyRoot.Domain.Interfaces.Services;
using MyRoot.Infrastructure.Persistence;
using MyRoot.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Interfaces;

namespace MyRoot.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.RegisterPersistence(connectionString);
            services.RegisterServices();

            return services;
        }

        private static IServiceCollection RegisterPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDBContext>(context =>
            {
                context.UseNpgsql(connectionString, options =>
                {
                    options.MigrationsHistoryTable("Migrations");
                });
            });

            services.AddScoped<IApplicationDBContext, ApplicationDBContext>();
            
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordService, PasswordService>();

            return services;
        }
    }
}
