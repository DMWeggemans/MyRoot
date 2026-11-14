using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.Steam
{
    public static partial class Steam
    {
        public class GetPlayerResponse
        {
            [JsonPropertyName("response")] public Payload Response { get; set; } = new Payload();

            public class Payload
            {
                [JsonPropertyName("players")] public List<PlayerSummary> Players { get; set; } = [];
            }
        }

        public class PlayerSummary
        {
            [JsonPropertyName("steamid")] public string SteamId { get; set; } = string.Empty;
            [JsonPropertyName("communityvisibilitystate")] public int CommunityVisibilityState { get; set; }
            [JsonPropertyName("profilestate")] public int ProfileState { get; set; }
            [JsonPropertyName("personaname")] public string PersonaName { get; set; } = string.Empty;
            [JsonPropertyName("profileurl")] public string ProfileUrl { get; set; } = string.Empty;
            [JsonPropertyName("avatar")] public string Avatar { get; set; } = string.Empty;
            [JsonPropertyName("avatarmedium")] public string AvatarMedium { get; set; } = string.Empty;
            [JsonPropertyName("avatarfull")] public string AvatarFull { get; set; } = string.Empty;
            [JsonPropertyName("avatarhash")] public string AvatarHash { get; set; } = string.Empty;
            [JsonPropertyName("lastlogoff")] public long LastLogOff { get; set; }
            [JsonPropertyName("personastate")] public int PersonaState { get; set; }
            [JsonPropertyName("realname")] public string RealName { get; set; } = string.Empty;
            [JsonPropertyName("primaryclanid")] public string PrimaryClanId { get; set; } = string.Empty;
            [JsonPropertyName("timecreated")] public long TimeCreated { get; set; }
            [JsonPropertyName("personastateflags")] public int PersonaStateFlags { get; set; }
            [JsonPropertyName("loccountrycode")] public string LocCountryCode { get; set; } = string.Empty;
        }
    }
}
