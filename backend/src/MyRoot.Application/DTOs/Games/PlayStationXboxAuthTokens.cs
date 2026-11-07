namespace MyRoot.Application.DTOs.Games
{
    public record struct PlayStationXboxAuthTokens(
        string AccessToken,
        DateTimeOffset AccessTokenExpiresOn,
        string RefreshToken,
        DateTimeOffset RefreshTokenExpiresOn
    ) { }
}
