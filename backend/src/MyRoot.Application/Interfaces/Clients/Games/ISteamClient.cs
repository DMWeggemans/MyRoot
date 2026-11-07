using MyRoot.Application.Responses.Games.Steam;

namespace MyRoot.Application.Interfaces.Clients.Games
{
    public interface ISteamClient
    {
        Task<SteamGetPlayerResponse?> GetPlayerAsync(string steamId);
        Task<SteamGetPlayerGamesResponse?> GetPlayerGamesAsync(string steamId);
    }
}
