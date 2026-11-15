using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.Requests.Games.Steam;
using MyRoot.Application.DTOs;
using MyRoot.Domain.Models;

namespace MyRoot.Application.Interfaces.Services.Games
{
    public interface ISteamService
    {
        Task<Result<string>> LinkCallbackAsync(OpenIdParameters parameters);
        Task<Result<LinkSteamResponse>> LinkPreviewAsync(LinkSteamRequest request);
        Task<Result> LinkSteamAsync(LinkSteamRequest request, Guid userId);
    }
}
