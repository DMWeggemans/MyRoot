using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Interfaces.Clients.Games;

namespace MyRoot.Application.Services.Games
{
    public class XboxService(IXboxClient _xboxClient) : IXboxService
    {

    }
}
