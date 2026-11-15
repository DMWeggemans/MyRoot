using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Extensions.Games.Steam;
using MyRoot.Application.Responses.Games.Steam;
using Microsoft.Extensions.Caching.Distributed;
using MyRoot.Application.Requests.Games.Steam;
using MyRoot.Domain.Interfaces.Clients.Games;
using MyRoot.Domain.Responses.Games.Steam;
using MyRoot.Application.DTOs.Games.Steam;
using MyRoot.Application.Extensions;
using MyRoot.Application.Interfaces;
using MyRoot.Domain.Configurations;
using MyRoot.Application.DTOs;
using MyRoot.Domain.Models;
using System.Text.Json;

namespace MyRoot.Application.Services.Games
{
    public class SteamService(IDistributedCache _cache, IHtmlTemplate _htmlTemplate, ISteamClient _steamClient) : ISteamService
    {
        public static readonly string AuthenticationTemplate = "Steam/GetSteamIdAsync/Authentication.html";
        private static readonly string SteamAuthCachePrefix = "steam:auth";
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<Result<string>> LinkCallbackAsync(OpenIdParameters parameters)
        {
            try
            {
                if (parameters.Mode is null)
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'PARAMETERS_NOT_FOUND', value: null }"
                    }));

                if (!await _steamClient.ValidateOpenIdAsync(parameters))
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, new Dictionary<string, string>()
                    {
                        ["Value"] = "{ succeeded: false, message: 'PARAMETERS_INVALID', value: null }",
                        ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                    }));

                string steamId = parameters.GetSteamId();
                //steamId = "76561199337224987";

                Guid key = Guid.NewGuid();
                Result<LinkSteamResponse> result = await GetLinkSteamResponseAsync(key, steamId);
                Dictionary<string, string> payload = new Dictionary<string, string>()
                {
                    ["Value"] = $"{JsonSerializer.Serialize(result, JsonOptions)}",
                    ["ApplicationUrl"] = ApplicationConfiguration.Urls.Application
                };

                if (!result.Succeeded)
                    return Result<string>.Failed(value: await _htmlTemplate.GetAsync(AuthenticationTemplate, payload));

                await _cache.SetObjectAsync($"{SteamAuthCachePrefix}-{key}", result.Value!.ToLinkPreviewRequest(), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(5));
                return Result<string>.Success(await _htmlTemplate.GetAsync(AuthenticationTemplate, payload));
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

        public async Task<Result<LinkSteamResponse>> LinkPreviewAsync(LinkSteamRequest request)
        {
            LinkSteamRequest? preview = await _cache.RemoveObjectAsync<LinkSteamRequest>($"{SteamAuthCachePrefix}-{request.Key}");
            if (preview is null || preview.SteamId != request.SteamId)
                return Result<LinkSteamResponse>.Failed("NOT_FOUND");

            Guid key = Guid.NewGuid();
            Result<LinkSteamResponse> response = await GetLinkSteamResponseAsync(key, preview.SteamId);

            if (response.Succeeded)
                await _cache.SetObjectAsync($"{SteamAuthCachePrefix}-{key}", response.Value!, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

            return response;
        }

        public async Task<Result> LinkSteamAsync(LinkSteamRequest request, Guid userId)
        {
            LinkSteamRequest? preview = await _cache.RemoveObjectAsync<LinkSteamRequest>($"{SteamAuthCachePrefix}-{request.Key}");
            if (preview is null || preview.SteamId != request.SteamId)
                return Result.Failed("NOT_FOUND");

            Result<LinkSteamResponse> response = await GetLinkSteamResponseAsync(Guid.Empty, preview.SteamId);
            if (!response.Succeeded || response.Value is null)
                return response;

            return Result.Success(new 
            {
                Provider = "Steam",
                UserId = response.Value.SteamId,
                UserName = response.Value.UserName,
                AccessToken = string.Empty,
                RefreshToken = string.Empty
            });
        }

        private async Task<Result<LinkSteamResponse>> GetLinkSteamResponseAsync(Guid key, string steamId)
        {
            Steam.GetPlayerResponse? playerResponse = await _steamClient.GetPlayerAsync(steamId);
            if (playerResponse is null)
                return Result<LinkSteamResponse>.Failed("PLAYER_NOT_FOUND");

            SteamPlayerSummary player = playerResponse.ToSteamPlayerSummary();
            Steam.GetPlayerGamesResponse? gamesResponse = await _steamClient.GetPlayerGamesAsync(steamId);
            if (gamesResponse is null)
                return Result<LinkSteamResponse>.Failed("GAMES_NOT_FOUND");

            List<SteamGameSummary> games = gamesResponse.ToSteamGameSummaries();
            LinkSteamResponse response = new LinkSteamResponse(key, steamId, player, games);

            return Result<LinkSteamResponse>.Success(response);
        }
    }
}
