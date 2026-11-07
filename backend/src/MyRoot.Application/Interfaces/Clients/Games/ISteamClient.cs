using MyRoot.Application.DTOs.Games.Steam;

namespace MyRoot.Application.Interfaces.Clients.Games
{
    public interface ISteamClient
    {
        Task<SteamPlayerSummary?> GetPlayerAsync(string steamId);
        Task<List<SteamGameSummary>> GetPlayerGamesAsync(string steamId);
    }
}
