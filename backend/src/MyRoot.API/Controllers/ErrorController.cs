using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;

namespace MyRoot.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            return StatusCode(500, Result.Failed("SERVER_ERROR"));
        }
    }
}
