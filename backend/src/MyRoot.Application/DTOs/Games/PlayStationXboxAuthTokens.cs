namespace MyRoot.Application.DTOs.Games
{
    public class PlayStationXboxAuthTokens
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTimeOffset AccessTokenExpiresOn { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset RefreshTokenExpiresOn { get; set; }
    }
}
