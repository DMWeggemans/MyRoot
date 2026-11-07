using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.Steam
{
    public record struct SteamGetPlayerResponse(
        [property: JsonPropertyName("response")] SteamGetPlayerResponse.SteamResponse Response
    )
    {
        public record struct SteamResponse(
            [property: JsonPropertyName("players")] List<SteamPlayerSummary> Players
        ) { }

        public record struct SteamPlayerSummary(
            [property: JsonPropertyName("steamid")] string SteamId,
            [property: JsonPropertyName("communityvisibilitystate")] int CommunityVisibilityState,
            [property: JsonPropertyName("profilestate")] int ProfileState,
            [property: JsonPropertyName("personaname")] string PersonaName,
            [property: JsonPropertyName("profileurl")] string ProfileUrl,
            [property: JsonPropertyName("avatar")] string Avatar,
            [property: JsonPropertyName("avatarmedium")] string AvatarMedium,
            [property: JsonPropertyName("avatarfull")] string AvatarFull,
            [property: JsonPropertyName("avatarhash")] string AvatarHash,
            [property: JsonPropertyName("lastlogoff")] long LastLogOff,
            [property: JsonPropertyName("personastate")] int PersonaState,
            [property: JsonPropertyName("realname")] string RealName,
            [property: JsonPropertyName("primaryclanid")] string PrimaryClanId,
            [property: JsonPropertyName("timecreated")] long TimeCreated,
            [property: JsonPropertyName("personastateflags")] int PersonaStateFlags,
            [property: JsonPropertyName("loccountrycode")] string LocCountryCode
        ) { }
    }
}
