using MyRoot.Application.DTOs;
using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.DTOs.Auth;

namespace MyRoot.Application.Interfaces.Services.Games
{
    public interface ISteamService
    {
        Task<Result<string>> GetSteamIdAsync(OpenIdParameters parameters);
        Task<Result<LinkSteamResponse>> GetLinkSteamResponseAsync(string steamId);
    }
}
