using MyRoot.Domain.Interfaces.Clients.Games;
using MyRoot.Domain.Responses.Games.Steam;
using Microsoft.Extensions.Configuration;
using MyRoot.Domain.Extensions;
using MyRoot.Domain.Models;
using System.Net.Http.Json;

namespace MyRoot.Infrastructure.Clients.Games
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

        public async Task<bool> ValidateOpenIdAsync(OpenIdParameters parameters)
        {
            parameters.Mode = "check_authentication";

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("https://steamcommunity.com/openid/login", parameters.ToFormUrlEncodedContent());
                string? body = await response.Content.ReadAsStringAsync();
                return body is not null && body.Contains("is_valid:true");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Steam.GetPlayerResponse?> GetPlayerAsync(string steamId)
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

                return await response.Content.ReadFromJsonAsync<Steam.GetPlayerResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Steam.GetPlayerGamesResponse?> GetPlayerGamesAsync(string steamId)
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

                return await response.Content.ReadFromJsonAsync<Steam.GetPlayerGamesResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
