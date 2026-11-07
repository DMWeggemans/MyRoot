using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.PlayStation
{
    public record struct PlayStationGetProfileByAccountIdResponse(
        [property: JsonPropertyName("onlineId")] string OnlineId,
        [property: JsonPropertyName("personalDetail")] PlayStationGetProfileByAccountIdResponse.PlayStationResponse PersonalDetail,
        [property: JsonPropertyName("aboutMe")] string AboutMe,
        [property: JsonPropertyName("avatars")] List<PlayStationGetProfileByAccountIdResponse.PlayStationAvatarResponse> Avatars,
        [property: JsonPropertyName("languages")] List<string> Languages,
        [property: JsonPropertyName("isPlus")] bool IsPlus,
        [property: JsonPropertyName("isOfficiallyVerified")] bool IsOfficiallyVerified,
        [property: JsonPropertyName("isMe")] bool IsMe
    )
    {
        public record struct PlayStationResponse(
            [property: JsonPropertyName("displayName")] string? DisplayName,
            [property: JsonPropertyName("firstName")] string? FirstName,
            [property: JsonPropertyName("lastName")] string? LastName,
            [property: JsonPropertyName("profilePictures")] List<PlayStationAvatarResponse>? ProfilePictures
        ) { }

        public record struct PlayStationAvatarResponse(
            [property: JsonPropertyName("size")] string Size,
            [property: JsonPropertyName("url")] string Url
        ) { }
    }
}
