using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.Steam
{
    public static partial class Steam
    {
        public class GetPlayerGamesResponse
        {
            public Payload Response { get; set; } = new Payload();

            public class Payload
            {
                public int GameCount { get; set; }
                public List<PlayerGameSummary> Games { get; set; } = [];
            }
        }

        public class PlayerGameSummary
        {
            [JsonPropertyName("appid")] public int AppId { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
            [JsonPropertyName("playtime_2weeks")] public int Playtime2Weeks { get; set; }
            [JsonPropertyName("playtime_forever")] public int PlaytimeForever { get; set; }
            [JsonPropertyName("img_icon_url")] public string ImgIconUrl { get; set; } = string.Empty;
            [JsonPropertyName("has_community_visible_stats")] public bool HasCommunityVisibleStats { get; set; }
            [JsonPropertyName("playtime_windows_forever")] public int PlaytimeWindowsForever { get; set; }
            [JsonPropertyName("playtime_mac_forever")] public int PlaytimeMacForever { get; set; }
            [JsonPropertyName("playtime_linux_forever")] public int PlaytimeLinuxForever { get; set; }
            [JsonPropertyName("playtime_deck_forever")] public int PlaytimeDeckForever { get; set; }
            [JsonPropertyName("rtime_last_played")] public long RTimeLastPlayed { get; set; }
            [JsonPropertyName("content_descriptorids")] public List<int> ContentDescriptorIds { get; set; } = [];
            [JsonPropertyName("playtime_disconnected")] public int PlaytimeDisconnected { get; set; }
        }
    }
}
