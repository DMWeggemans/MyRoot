using MyRoot.Application.DTOs.Games;
using MyRoot.Application.Responses.Games.Xbox;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class XboxAuthTokensResponseExtensions
    {
        public static PlayStationXboxAuthTokens ToPlayStationXboxAuthTokens(this XboxAuthTokensResponse response)
            => new PlayStationXboxAuthTokens()
            {
                AccessToken = response.AccessToken,
                AccessTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.AccessTokenExpiresIn),
                RefreshToken = response.RefreshToken,
                RefreshTokenExpiresOn = DateTimeOffset.UtcNow.AddMinutes((90 * 24 * 60) - 5)
            };
    }
}
