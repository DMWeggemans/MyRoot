using MyRoot.Application.DTOs.Games.PlayStation;

namespace MyRoot.Application.Interfaces.Clients.Games
{
    public interface IPlayStationClient
    {
        Task<string?> GetCodeByNpssoAsync(string npsso);
        Task<PlayStationAuthTokens?> GetTokensByCodeAsync(string code);
        Task<PlayStationAuthTokens?> RefreshTokensAsync(string refreshToken);

        Task<PlayStationUserIds?> GetAccountIdByUserNameAsync(string accessToken, string userName);
        Task<PlayStationProfile?> GetProfileByAccountIdAsync(string accessToken, string accountId);
    }
}
