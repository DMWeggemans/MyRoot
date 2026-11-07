using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Extensions.Games.Steam
{
    public static class SteamGetPlayerResponseExtensions
    {
        public static SteamPlayerSummary? ToSteamPlayerSummary(this SteamGetPlayerResponse? response)
        {
            if (response is null)
                return null;

            SteamGetPlayerResponse.SteamPlayerSummary player = response.Value.Response.Players[0];
            return new SteamPlayerSummary()
            {
                SteamId = player.SteamId,
                UserName = player.PersonaName,
                Avatar = player.AvatarFull,
                ProfileUrl = player.ProfileUrl,
                IsPublic = player.CommunityVisibilityState == 3,
                LastLogOff = DateTimeOffset.FromUnixTimeSeconds(player.LastLogOff),
                TimeCreated = DateTimeOffset.FromUnixTimeSeconds(player.TimeCreated)
            };
        }
    }
}
