using MyRoot.Application.Interfaces.Services.Watchables;
using MyRoot.Domain.Interfaces;

namespace MyRoot.Application.Services.Watchables
{
    public class WatchableService : IWatchableService
    {

        private readonly IApplicationDBContext Context;

        public WatchableService(IApplicationDBContext context)
        {
            Context = context;
        }
    }
}
