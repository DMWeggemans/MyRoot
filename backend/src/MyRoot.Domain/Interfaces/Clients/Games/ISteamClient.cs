using MyRoot.Domain.Responses.Games.Steam;
using MyRoot.Domain.Models;

namespace MyRoot.Domain.Interfaces.Clients.Games
{
    public interface ISteamClient
    {
        Task<bool> ValidateOpenIdAsync(OpenIdParameters parameters);
        Task<Steam.GetPlayerResponse?> GetPlayerAsync(string steamId);
        Task<Steam.GetPlayerGamesResponse?> GetPlayerGamesAsync(string steamId);
    }
}
