using MyRoot.Domain.Responses.Games.PlayStation;

namespace MyRoot.Domain.Interfaces.Clients.Games
{
    public interface IPlayStationClient
    {
        Task<string?> GetCodeByNpssoAsync(string npsso);
        Task<PlayStation.AuthTokensResponse?> GetTokensByCodeAsync(string code);
        Task<PlayStation.AuthTokensResponse?> RefreshTokensAsync(string refreshToken);
        Task<PlayStation.GetAccountIdByUserNameResponse?> GetAccountIdByUserNameAsync(string accessToken, string userName);
        Task<PlayStation.GetProfileByAccountIdResponse?> GetProfileByAccountIdAsync(string accessToken, string accountId);
        Task<PlayStation.GetTrophyTitlesByAccountIdResponse?> GetTrophyTitlesByAccountIdAsync(string accessToken, string accountId, int limit = 100, int offset = 0);
    }
}
