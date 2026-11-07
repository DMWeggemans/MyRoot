using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.Steam
{
    public record struct SteamGetPlayerGamesResponse(
        [property: JsonPropertyName("response")] SteamGetPlayerGamesResponse.SteamResponse Response
    )
    {
        public record struct SteamResponse(
            [property: JsonPropertyName("game_count")] int GameCount,
            [property: JsonPropertyName("games")] List<SteamPlayerGameSummary>? Games
        ) { }

        public record struct SteamPlayerGameSummary(
            [property: JsonPropertyName("appid")] int AppId,
            [property: JsonPropertyName("name")] string Name,
            [property: JsonPropertyName("playtime_2weeks")] int Playtime2Weeks,
            [property: JsonPropertyName("playtime_forever")] int PlaytimeForever,
            [property: JsonPropertyName("img_icon_url")] string ImgIconUrl,
            [property: JsonPropertyName("has_community_visible_stats")] bool HasCommunityVisibleStats,
            [property: JsonPropertyName("playtime_windows_forever")] int PlaytimeWindowsForever,
            [property: JsonPropertyName("playtime_mac_forever")] int PlaytimeMacForever,
            [property: JsonPropertyName("playtime_linux_forever")] int PlaytimeLinuxForever,
            [property: JsonPropertyName("playtime_deck_forever")] int PlaytimeDeckForever,
            [property: JsonPropertyName("rtime_last_played")] long RTimeLastPlayed,
            [property: JsonPropertyName("content_descriptorids")] List<int> ContentDescriptorIds,
            [property: JsonPropertyName("playtime_disconnected")] int PlaytimeDisconnected
        ) { }
    }
}
