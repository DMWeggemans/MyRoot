using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.PlayStation
{
    public static partial class PlayStation
    {
        public class AuthTokensResponse
        {
            [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
            [JsonPropertyName("token_type")] public string TokenType { get; set; } = string.Empty;
            [JsonPropertyName("expires_in")] public long AccessTokenExpiresIn { get; set; }
            [JsonPropertyName("scope")] public string Scope { get; set; } = string.Empty;
            [JsonPropertyName("id_token")] public string IdToken { get; set; } = string.Empty;
            [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; } = string.Empty;
            [JsonPropertyName("refresh_token_expires_in")] public long RefreshTokenExpiresIn { get; set; }
        }
    }
}
