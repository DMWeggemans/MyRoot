using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Interfaces.Clients.Games;

namespace MyRoot.Application.Services.Games
{
    public class PlayStationService(IPlayStationClient _playStationClient) : IPlayStationService
    {

    }
}
