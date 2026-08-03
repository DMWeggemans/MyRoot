using Microsoft.OpenApi;

namespace MyRoot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new OpenApiInfo()
                {
                    Title = "API",
                    Version = null
                });
            });

            var app = builder.Build();

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
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
