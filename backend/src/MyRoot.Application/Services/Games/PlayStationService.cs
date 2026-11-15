using Microsoft.Extensions.Caching.Distributed;
using MyRoot.Application.DTOs;
using MyRoot.Application.DTOs.Games;
using MyRoot.Application.Extensions;
using MyRoot.Application.Extensions.Games.PlayStation;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Requests.Games.PlayStation;
using MyRoot.Application.Responses.Games.PlayStation;
using MyRoot.Domain.Interfaces.Clients.Games;
using MyRoot.Domain.Responses.Games.PlayStation;

namespace MyRoot.Application.Services.Games
{
    public class PlayStationService(IPlayStationClient _playStationClient, IDistributedCache _cache) : IPlayStationService
    {
        private readonly static string NPSSO = "P4Lgqcrnd5VoiM4mv4ETB4CZq0YnJnYpoqt1izUZKlCp8p9d1BW48yiJZSUMgSP4";
        private readonly static string PlayStationAuthPrefix = "playstation:auth";

        public async Task<Result<LinkPlayStationResponse>> GetProfileByOnlineIdAsync(string onlineId)
            => await GetProfileAsync(NPSSO, onlineId);

        public async Task<Result<LinkPlayStationResponse>> GetProfileByNpssoAsync(string npsso)
            => await GetProfileAsync(npsso, "me");

        private async Task<Result<LinkPlayStationResponse>> GetProfileAsync(string npsso, string onlineId)
        {
            Result<PlayStationXboxAuthTokens> tokens = await GetAuthTokensAsync(npsso);
            if (!tokens.Succeeded || tokens.Value is null)
                return Result<LinkPlayStationResponse>.Failed("INVALID_NPSSO");

            PlayStation.GetAccountIdByUserNameResponse? ids = await _playStationClient.GetAccountIdByUserNameAsync(tokens.Value.AccessToken, onlineId);
            if (ids is null)
                return Result<LinkPlayStationResponse>.Failed("USER_NOT_FOUND");

            PlayStation.GetProfileByAccountIdResponse? user = await _playStationClient.GetProfileByAccountIdAsync(tokens.Value.AccessToken, ids.Profile.AccountId);
            if (user is null)
                return Result<LinkPlayStationResponse>.Failed("USER_NOT_FOUND");

            PlayStation.GetTrophyTitlesByAccountIdResponse? trophyTitles = null;
            List<PlayStation.GetTrophyTitlesByAccountIdResponse.Payload> games = [];
            bool hasGamesPrivate = false;
            do
            {
                int offset = trophyTitles?.NextOffset ?? 0;
                trophyTitles = await _playStationClient.GetTrophyTitlesByAccountIdAsync(tokens.Value.AccessToken, ids.Profile.AccountId, 800, offset);
                if (trophyTitles is null && offset <= 0)
                {
                    hasGamesPrivate = true;
                    break;
                }

                if (trophyTitles is null)
                    continue;

                games.AddRange(trophyTitles.TrophyTitles);
            }
            while (trophyTitles?.NextOffset > 0);

            Guid key = Guid.NewGuid();
            LinkPlayStationResponse response = user.ToPlayStationProfile(key, ids.Profile.AccountId, games, hasGamesPrivate);
            await _cache.SetObjectAsync($"{PlayStationAuthPrefix}-{key}", new LinkPlayStationRequest()
            {
                Key = key,
                AccountId = response.AccountId,
                AccessToken = tokens.Value.AccessToken,
                RefreshToken = tokens.Value.RefreshToken
            }, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(5));

            return Result<LinkPlayStationResponse>.Success(response);
        }

        private async Task<Result<PlayStationXboxAuthTokens>> GetAuthTokensAsync(string npsso)
        {
            string? code = await _playStationClient.GetCodeByNpssoAsync(npsso);
            if (code is null)
                return Result<PlayStationXboxAuthTokens>.Failed();

            PlayStation.AuthTokensResponse? response = await _playStationClient.GetTokensByCodeAsync(code);
            if (response is null)
                return Result<PlayStationXboxAuthTokens>.Failed();

            return Result<PlayStationXboxAuthTokens>.Success(response.ToPlayStationXboxAuthTokens());
        }
    }
}
