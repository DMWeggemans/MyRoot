using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.Xbox
{
    public static partial class Xbox
    {
        public class AuthTokensResponse
        {
            [JsonPropertyName("token_type")] public string TokenType { get; set; } = string.Empty;
            [JsonPropertyName("expires_in")] public long AccessTokenExpiresIn { get; set; }
            [JsonPropertyName("scope")] public string Scope { get; set; } = string.Empty;
            [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
            [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; } = string.Empty;
            [JsonPropertyName("user_id")] public string UserId { get; set; } = string.Empty;
            [JsonPropertyName("error")] public string? Error { get; set; }
            [JsonPropertyName("error_description")] public string? ErrorDescription { get; set; }
            [JsonPropertyName("correlation_id")] public string? CorrelationId { get; set; }

            public bool IsInvalid => Error == "grant_invalid";
            public bool IsPending => Error == "authorization_pending";
        }
    }
}
