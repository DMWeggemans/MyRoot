using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.PlayStation
{
    public record struct PlayStationAuthTokensResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("token_type")] string TokenType,
        [property: JsonPropertyName("expires_in")] long AccessTokenExpiresIn,
        [property: JsonPropertyName("scope")] string Scope,
        [property: JsonPropertyName("id_token")] string IdToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken,
        [property: JsonPropertyName("refresh_token_expires_in")] long RefreshTokenExpiresIn
    ) { }
}
