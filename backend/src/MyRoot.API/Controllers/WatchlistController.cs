using MyRoot.Application.Requests.Auth;
using Microsoft.AspNetCore.Mvc;
using MyRoot.API.Swagger;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchlistController : ControllerBase
    {
        [HttpQuery]
        public async Task<IActionResult> GetWatchables(LoginUserRequest request)
        {
            return StatusCode(500, request);
        }
    }
}
