using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.Xbox
{
    public record struct XboxAuthTokensResponse(
        [property: JsonPropertyName("token_type")] string TokenType,
        [property: JsonPropertyName("expires_in")] long AccessTokenExpiresIn,
        [property: JsonPropertyName("scope")] string Scope,
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken,
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("error")] string? Error,
        [property: JsonPropertyName("error_description")] string? ErrorDescription,
        [property: JsonPropertyName("correlation_id")] string? CorrelationId
    )
    {
        public readonly bool IsInvalid => Error == "grant_invalid";
        public readonly bool IsPending => Error == "authorization_pending";
    }
}
