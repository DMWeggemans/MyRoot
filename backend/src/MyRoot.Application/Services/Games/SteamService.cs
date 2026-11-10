using MyRoot.Application.DTOs;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.DTOs.Games.Steam;
using MyRoot.Application.Extensions.Games.Steam;
using MyRoot.Application.Interfaces;
using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Domain.Configurations;
using System.Text.Json;

namespace MyRoot.Application.Services.Games
{
    public class SteamService(HttpClient _httpClient, IHtmlTemplate _htmlTemplate, ISteamClient _steamClient) : ISteamService
    {
        public static readonly string AuthenticationTemplate = "Steam/GetSteamIdAsync/Authentication.html";
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<Result<string>> GetSteamIdAsync(OpenIdParameters parameters)
        {
            try
            {
                if (parameters.Mode is null)
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'PARAMETERS_NOT_FOUND', value: null }"
                    }));

                parameters.Mode = "check_authentication";
                using var content = parameters.ToFormUrlEncodedContent();
                HttpResponseMessage response = await _httpClient.PostAsync("https://steamcommunity.com/openid/login", content);

                string? body = await response.Content.ReadAsStringAsync();
                if (body is null || !body.Contains("is_valid:true"))
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'PARAMETERS_INVALID', value: null }",
                        ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                    }));

                //string steamId = "76561199337224987";
                string steamId = parameters.GetSteamId();
                SteamGetPlayerResponse? playerResponse = await _steamClient.GetPlayerAsync(steamId);
                if (playerResponse is null)
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'PRIVATE_PLAYER', value: null }",
                        ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                    }));

                SteamPlayerSummary player = playerResponse.Value.ToSteamPlayerSummary();
                SteamGetPlayerGamesResponse? gamesResponse = await _steamClient.GetPlayerGamesAsync(steamId);
                if (gamesResponse is null)
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'NO_GAMES_FOUND', value: null }",
                        ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                    }));

                List<SteamGameSummary> games = gamesResponse.Value.ToSteamGameSummaries();

                string payload = JsonSerializer.Serialize(new LinkSteamResponse(steamId, player, games), JsonOptions);
                return Result<string>.Success(await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                {
                    ["Value"] = $"{{ succeeded: true, message: null, value: {payload} }}",
                    ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                }));
            }
            catch (Exception)
            {
                return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                {
                    ["Value"] = "{ succeeded: false, message: 'ERROR', value: null }",
                    ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                }));
            }
        }
    }
}
