using MyRoot.Application.Interfaces.Services.Watchables;
using Microsoft.AspNetCore.Mvc;
using MyRoot.API.Swagger;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchablesController : ControllerBase
    {
        private readonly IWatchableService WatchableService;

        public WatchablesController(IWatchableService watchableService)
        {
            WatchableService = watchableService;
        }

        [HttpQuery]
        public async Task<IActionResult> GetWatchables()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList()
        {
            throw new NotImplementedException();
        }
    }
}
