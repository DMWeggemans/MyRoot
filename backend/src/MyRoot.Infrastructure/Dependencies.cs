using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRoot.Domain.Interfaces;
using MyRoot.Domain.Interfaces.Services;
using MyRoot.Infrastructure.Persistence;
using MyRoot.Infrastructure.Services;

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
                context.UseSqlServer(connectionString, options =>
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
