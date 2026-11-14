using Response = MyRoot.Domain.Responses.Games.Xbox.Xbox;
using MyRoot.Application.DTOs.Games;

namespace MyRoot.Application.Extensions.Games.Xbox
{
    public static class AuthTokensResponseExtensions
    {
        public static PlayStationXboxAuthTokens ToPlayStationXboxAuthTokens(this Response.AuthTokensResponse response)
            => new PlayStationXboxAuthTokens()
            {
                AccessToken = response.AccessToken,
                AccessTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.AccessTokenExpiresIn),
                RefreshToken = response.RefreshToken,
                RefreshTokenExpiresOn = DateTimeOffset.UtcNow.AddMinutes((90 * 24 * 60) - 5)
            };
    }
}
