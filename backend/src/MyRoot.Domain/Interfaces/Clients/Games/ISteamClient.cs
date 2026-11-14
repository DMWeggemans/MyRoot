using MyRoot.Domain.Responses.Games.Steam;

namespace MyRoot.Domain.Interfaces.Clients.Games
{
    public interface ISteamClient
    {
        Task<Steam.GetPlayerResponse?> GetPlayerAsync(string steamId);
        Task<Steam.GetPlayerGamesResponse?> GetPlayerGamesAsync(string steamId);
    }
}
