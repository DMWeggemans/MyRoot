using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.PlayStation
{
    public static partial class PlayStation
    {
        public class GetAccountIdByUserNameResponse
        {
            [JsonPropertyName("profile")] public Payload Profile { get; set; } = new Payload();

            public class Payload
            {
                [JsonPropertyName("onlineId")] public string OnlineId { get; set; } = string.Empty;
                [JsonPropertyName("accountId")] public string AccountId { get; set; } = string.Empty;
                [JsonPropertyName("currentOnlineId")] public string? CurrentOnlineId { get; set; }
            }
        }
    }
}
