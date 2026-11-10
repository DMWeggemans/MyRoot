using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Extensions.Games.Steam;
using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.Extensions.Common;
using MyRoot.Application.DTOs.Games.Steam;
using Microsoft.Extensions.Configuration;
using MyRoot.Application.Extensions;
using System.Net.Http.Json;

namespace MyRoot.Application.Clients.Games
{
    public class SteamClient : ISteamClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public SteamClient(HttpClient httpClient, IConfiguration configuration)
        {
            httpClient.BaseAddress = Uri.TryCreate("https://api.steampowered.com", UriKind.Absolute, out Uri? uri)
                ? uri : throw new InvalidOperationException();

            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<SteamGetPlayerResponse?> GetPlayerAsync(string steamId)
        {
            string key = _configuration.GetValueOrThrow("APIs:SteamAPI:Key");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(URL.Create("/ISteamUser/GetPlayerSummaries/v0002")
                    .WithParameter("key", key)
                    .WithParameter("steamids", steamId)
                    .ToString());
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<SteamGetPlayerResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SteamGetPlayerGamesResponse?> GetPlayerGamesAsync(string steamId)
        {
            string key = _configuration.GetValueOrThrow("APIs:SteamAPI:Key");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(URL.Create("/IPlayerService/GetOwnedGames/v0001")
                    .WithParameter("key", key)
                    .WithParameter("steamid", steamId.ToString())
                    .WithParameter("format", "json")
                    .WithParameter("include_appinfo", "true")
                    .WithParameter("include_played_free_games", "true")
                    .ToString());
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<SteamGetPlayerGamesResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
