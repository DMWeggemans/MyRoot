using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MyRoot.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetObjectAsync<TObject>(this IDistributedCache cache, string key, TObject value, TimeSpan expiration, TimeSpan? refresh = null)
        {
            string json = JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expiration,
                SlidingExpiration = refresh is null ? TimeSpan.Zero : refresh
            });
        }

        public static async Task<TObject?> GetObjectAsync<TObject>(this IDistributedCache cache, string key)
        {
            string? json = await cache.GetStringAsync(key);
            if (json is null)
                return default;

            return JsonSerializer.Deserialize<TObject>(json);
        }

        public static async Task<TObject?> RemoveObjectAsync<TObject>(this IDistributedCache cache, string key)
        {
            TObject? value = await cache.GetObjectAsync<TObject>(key);
            if (value is null)
                return default;

            await cache.RemoveAsync(key);
            return value;
        }
    }
}
