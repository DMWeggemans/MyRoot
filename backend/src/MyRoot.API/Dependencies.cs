using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using MyRoot.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;
using Microsoft.OpenApi;
using System.Text.Json;
using System.Text;

namespace MyRoot.API
{
    public static class Dependencies
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly string UnauthorizedPayload = JsonSerializer.Serialize(Result.Failed("NOT_AUTHORIZED"), JsonOptions);

        public static IServiceCollection RegisterAPI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.RegisterSwagger();
            services.RegisterConfigurations();
            services.RegisterAuthentication(configuration);
            services.RegisterAuthorization();
            services.RegisterCors(configuration, environment);

            return services;
        }

        private static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new OpenApiInfo()
                {
                    Title = "API",
                    Version = null
                });
            });

            return services;
        }

        private static IServiceCollection RegisterConfigurations(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        private static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationBuilder builder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                byte[] secret = Encoding.UTF8.GetBytes(configuration.GetValueOrThrow("JWT:Secret"));

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValueOrThrow("JWT:Issuer"),

                    ValidateAudience = true,
                    ValidAudience = configuration.GetValueOrThrow("JWT:Audience"),

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = (context) =>
                    {
                        string? token = context.Request.Cookies["X-Access-Token"];
                        if (!string.IsNullOrEmpty(token))
                            context.Token = token;

                        return Task.CompletedTask;
                    },

                    OnChallenge = async (context) =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(UnauthorizedPayload);
                    }
                };
            });

            return services;
        }

        private static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();

            return services;
        }

        private static IServiceCollection RegisterCors(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    List<string> origins = [configuration.GetValueOrThrow("UI:Url")];
                    if (environment.IsDevelopment())
                        origins.Add("http://localhost:4200");

                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins([..origins]);
                });
            });

            return services;
        }
    }
}
