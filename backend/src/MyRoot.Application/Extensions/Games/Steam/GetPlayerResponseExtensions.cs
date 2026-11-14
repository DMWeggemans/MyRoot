using Response = MyRoot.Domain.Responses.Games.Steam.Steam;
using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Extensions.Games.Steam
{
    public static class GetPlayerResponseExtensions
    {
        public static SteamPlayerSummary ToSteamPlayerSummary(this Response.GetPlayerResponse response)
        {
            Response.PlayerSummary? player = response.Response.Players.FirstOrDefault();
            if (player is null)
                return new SteamPlayerSummary();

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
