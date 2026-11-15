using MyRoot.Application.DTOs;
using MyRoot.Application.Responses.Games.PlayStation;

namespace MyRoot.Application.Interfaces.Services.Games
{
    public interface IPlayStationService
    {
        Task<Result<LinkPlayStationResponse>> GetProfileByOnlineIdAsync(string onlineId);
        Task<Result<LinkPlayStationResponse>> GetProfileByNpssoAsync(string npsso);
    }
}
