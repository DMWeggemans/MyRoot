namespace MyRoot.Application.DTOs.Games.PlayStation
{
    public record struct PlayStationAuthTokens(
        string AccessToken,
        DateTimeOffset AccessTokenExpiresOn,
        string RefreshToken,
        DateTimeOffset RefreshTokenExpiresOn
    ) { }
}
