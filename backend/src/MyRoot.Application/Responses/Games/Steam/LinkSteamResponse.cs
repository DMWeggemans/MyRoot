using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Responses.Games.Steam
{
    public class LinkSteamResponse
    {
        public string SteamId { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public DateTimeOffset LastLogOff  { get; set; }
        public DateTimeOffset TimeCreated { get; set; }

        public LinkSteamResponse(string steamId, SteamPlayerSummary? player, List<SteamGameSummary> games)
        {
            SteamId = steamId;
            IsPublic = (player is null || player.Value.IsPublic) && games.Count != 0;
            UserName = player?.UserName ?? string.Empty;
            ProfileUrl = player?.ProfileUrl ?? string.Empty;
            Avatar = player?.Avatar ?? string.Empty;
            LastLogOff = player?.LastLogOff ?? DateTimeOffset.MinValue;
            TimeCreated = player?.TimeCreated ?? DateTimeOffset.MinValue;
        }
    }
}
