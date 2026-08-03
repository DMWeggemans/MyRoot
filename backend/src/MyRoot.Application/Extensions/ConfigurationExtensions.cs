using Microsoft.Extensions.Configuration;

namespace MyRoot.Application.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetValueOrThrow(this IConfiguration configuration, string key)
            => configuration[key] ?? configuration.Throw($"The environment variable: [{key}] is not found!");

        public static string GetConnectionStringOrThrow(this IConfiguration configuration, string key)
            => configuration.GetValueOrThrow($"ConnectionStrings:{key}");

        public static string Throw(this IConfiguration configuration, string key)
            => throw new InvalidOperationException($"The environment variable: [{key}] is not found!");
    }
}
