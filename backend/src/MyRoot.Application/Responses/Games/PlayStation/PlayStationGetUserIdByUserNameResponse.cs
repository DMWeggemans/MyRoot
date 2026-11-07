using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.PlayStation
{
    public record struct PlayStationGetUserIdByUserNameResponse(
        [property: JsonPropertyName("profile")] PlayStationGetUserIdByUserNameResponse.PlayStationResponse Profile
    )
    {
        public record struct PlayStationResponse(
            [property: JsonPropertyName("onlineId")] string OnlineId,
            [property: JsonPropertyName("accountId")] string AccountId,
            [property: JsonPropertyName("currentOnlineId")] string? CurrentOnlineId
        ) { }
    }
}
