using Response = MyRoot.Domain.Responses.Games.Steam.Steam;
using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Extensions.Games.Steam
{
    public static class GetPlayerGamesResponseExtensions
    {
        public static List<SteamGameSummary> ToSteamGameSummaries(this Response.GetPlayerGamesResponse response)
            => [.. (response.Response.Games ?? []).Select(x => x.ToSteamGameSummary())];

        public static SteamGameSummary ToSteamGameSummary(this Response.PlayerGameSummary response)
            => new SteamGameSummary()
            {
                AppId = response.AppId,
                Name = response.Name,
                PlaytimeInMinutes = response.PlaytimeForever,
                LastPlayed = DateTimeOffset.FromUnixTimeSeconds(response.RTimeLastPlayed)
            };
    }
}
