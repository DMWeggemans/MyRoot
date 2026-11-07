using MyRoot.Application.Responses.Games.PlayStation;
using MyRoot.Application.DTOs.Games;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class PlayStationAuthTokensResponseExtensions
    {
        public static PlayStationXboxAuthTokens ToPlayStationXboxAuthTokens(this PlayStationAuthTokensResponse response)
            => new PlayStationXboxAuthTokens()
            {
                AccessToken = response.AccessToken,
                AccessTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.AccessTokenExpiresIn),
                RefreshToken = response.RefreshToken,
                RefreshTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.RefreshTokenExpiresIn)
            };
    }
}
