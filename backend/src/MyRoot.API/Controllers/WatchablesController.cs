using Microsoft.AspNetCore.Mvc;
using MyRoot.API.Swagger;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchablesController : ControllerBase
    {
        [HttpQuery]
        public async Task<IActionResult> GetWatchables()
        {
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWatchable()
        {
            return StatusCode(500);
        }

        [HttpQuery("Seasons")]
        public async Task<IActionResult> GetSeasons()
        {
            return StatusCode(500);
        }

        [HttpPost("Seasons")]
        public async Task<IActionResult> CreateSeason()
        {
            return StatusCode(500);
        }

        [HttpQuery("Episodes")]
        public async Task<IActionResult> GetEpisodes()
        {
            return StatusCode(500);
        }

        [HttpPost("Episodes")]
        public async Task<IActionResult> CreateEpisode()
        {
            return StatusCode(500);
        }
    }
}
