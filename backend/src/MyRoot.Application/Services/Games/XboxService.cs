using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Domain.Interfaces.Clients.Games;

namespace MyRoot.Application.Services.Games
{
    public class XboxService(IXboxClient _xboxClient) : IXboxService
    {

    }
}
