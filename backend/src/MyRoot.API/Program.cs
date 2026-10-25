using MyRoot.Infrastructure.Persistence;
using MyRoot.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Configurations;
using MyRoot.Infrastructure;
using MyRoot.Application;

namespace MyRoot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ApplicationConfiguration.Urls.Application = builder.Configuration.GetValueOrThrow("Urls:Application");
            ApplicationConfiguration.Urls.API = builder.Configuration.GetValueOrThrow("Urls:API");

            // Add services to the container.
            builder.Services.RegisterAPI(builder.Configuration, builder.Environment);
            builder.Services.RegisterApplication();
            builder.Services.RegisterInfrastructure(builder.Configuration.GetConnectionStringOrThrow("DefaultConnection"));

            builder.Services.AddControllers();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "{documentName}/specifications.json";
                });

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("specifications.json", "API");
                    options.DefaultModelsExpandDepth(-1);
                    options.RoutePrefix = "api";
                });

                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/api");
                    return Task.CompletedTask;
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
            }

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
