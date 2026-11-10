using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.DTOs;

namespace MyRoot.Application.Interfaces.Services.Games
{
    public interface ISteamService
    {
        Task<Result<string>> GetSteamIdAsync(OpenIdParameters parameters);
    }
}
