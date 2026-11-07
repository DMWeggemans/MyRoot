using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Extensions.Games.Steam
{
    public static class SteamGetPlayerGamesResponseExtensions
    {
        public static SteamGameSummary ToSteamGameSummary(this SteamGetPlayerGamesResponse.SteamPlayerGameSummary response)
            => new SteamGameSummary()
            {
                AppId = response.AppId,
                Name = response.Name,
                PlaytimeInMinutes = response.PlaytimeForever,
                LastPlayed = DateTimeOffset.FromUnixTimeSeconds(response.RTimeLastPlayed)
            };

        public static List<SteamGameSummary> ToSteamGameSummaries(this SteamGetPlayerGamesResponse? response)
        {
            if (response is null)
                return [];

            return [..(response.Value.Response.Games ?? []).Select(x => x.ToSteamGameSummary())];
        }
    }
}
