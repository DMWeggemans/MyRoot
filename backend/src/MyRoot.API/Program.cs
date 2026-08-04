using MyRoot.Infrastructure.Persistence;
using MyRoot.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using MyRoot.Infrastructure;
using MyRoot.Application;

namespace MyRoot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                    options.RoutePrefix = "api";
                });

                app.MapGet("/", (HttpContext context) =>
                {
                    context.Response.Redirect("/api");
                    return Task.CompletedTask;
                });
            }

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseExceptionHandler("/error");

            app.Run();
        }
    }
}
