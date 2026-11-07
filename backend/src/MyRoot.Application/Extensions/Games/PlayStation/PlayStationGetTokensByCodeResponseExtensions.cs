using MyRoot.Application.DTOs.Games.PlayStation;
using MyRoot.Application.Responses.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class PlayStationGetTokensByCodeResponseExtensions
    {
        public static PlayStationAuthTokens ToPlayStationAuthTokens(this PlayStationGetTokensByCodeResponse response)
            => new PlayStationAuthTokens()
            {
                AccessToken = response.AccessToken,
                AccessTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.AccessTokenExpiresIn),
                RefreshToken = response.RefreshToken,
                RefreshTokenExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.RefreshTokenExpiresIn)
            };
    }
}
