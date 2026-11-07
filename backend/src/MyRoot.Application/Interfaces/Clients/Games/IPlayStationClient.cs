using MyRoot.Application.Responses.Games.PlayStation;

namespace MyRoot.Application.Interfaces.Clients.Games
{
    public interface IPlayStationClient
    {
        Task<string?> GetCodeByNpssoAsync(string npsso);
        Task<PlayStationAuthTokensResponse?> GetTokensByCodeAsync(string code);
        Task<PlayStationAuthTokensResponse?> RefreshTokensAsync(string refreshToken);
        Task<PlayStationGetUserIdByUserNameResponse?> GetAccountIdByUserNameAsync(string accessToken, string userName);
        Task<PlayStationGetProfileByAccountIdResponse?> GetProfileByAccountIdAsync(string accessToken, string accountId);
    }
}
