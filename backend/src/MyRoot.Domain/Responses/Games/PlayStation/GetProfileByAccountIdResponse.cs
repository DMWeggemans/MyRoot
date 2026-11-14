using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.PlayStation
{
    public static partial class PlayStation
    {
        public class GetProfileByAccountIdResponse
        {
            [JsonPropertyName("onlineId")] public string OnlineId { get; set; } = string.Empty;
            [JsonPropertyName("personalDetail")] public Payload PersonalDetail { get; set; } = new Payload();
            [JsonPropertyName("aboutMe")] public string AboutMe { get; set; } = string.Empty;
            [JsonPropertyName("avatars")] public List<Avatar> Avatars { get; set; } = [];
            [JsonPropertyName("languages")] public List<string> Languages { get; set; } = [];
            [JsonPropertyName("isPlus")] public bool IsPlus { get; set; }
            [JsonPropertyName("isOfficiallyVerified")] public bool IsOfficiallyVerified { get; set; }
            [JsonPropertyName("isMe")] public bool IsMe { get; set; }

            public class Payload
            {
                [JsonPropertyName("displayName")] public string? DisplayName { get; set; }
                [JsonPropertyName("firstName")] public string? FirstName { get; set; }
                [JsonPropertyName("lastName")] public string? LastName { get; set; }
                [JsonPropertyName("profilePictures")] public List<Avatar> ProfilePictures { get; set; } = [];
            }
        }

        public class Avatar
        {
            [JsonPropertyName("size")] public string Size { get; set; } = string.Empty;
            [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
        }
    }
}
