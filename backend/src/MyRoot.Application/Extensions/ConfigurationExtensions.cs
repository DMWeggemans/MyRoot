using Microsoft.Extensions.Configuration;

namespace MyRoot.Application.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetValueOrThrow(this IConfiguration configuration, string key)
            => configuration[key] ?? configuration.Throw(key);

        public static string GetConnectionStringOrThrow(this IConfiguration configuration, string key)
        {
            IConfigurationSection section = configuration.GetSection($"Databases:{key}");
            string location = section.GetValueOrThrow("Location");
            string[] locations = location.Split(':');
            int port = 5432;

            if (locations.Length > 1 && int.TryParse(locations[1], out int _port))
            {
                port = _port;
                location = locations[0];
            }

            string name = section.GetValueOrThrow("Name");
            string user = section.GetValueOrThrow("User");
            string password = section.GetValueOrThrow("Password");

            return $"Server={location};Port={port};Database={name};Username={user};Password={password};";
        }

        public static string Throw(this IConfiguration configuration, string key)
            => throw new InvalidOperationException($"The environment variable: [{key}] is not found!");
    }
}
