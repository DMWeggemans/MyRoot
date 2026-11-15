using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.Requests.Games.Steam;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;
using System.Security.Claims;
using MyRoot.Domain.Models;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SteamController(ISteamService _steamService) : ControllerBase
    {
        [HttpGet("Auth/Callback")]
        public async Task<IActionResult> LinkCallback()
        {
            OpenIdParameters parameters = OpenIdParameters.Build(Request.Query.ToDictionary());
            Result<string> result = await _steamService.LinkCallbackAsync(parameters);
            return Content(result.Value!, "text/html");
        }

        [Route("Auth/Preview")]
        [AcceptVerbs("QUERY")]
        [SwaggerIgnore]
        public async Task<IActionResult> LinkPreview(LinkSteamRequest request)
        {
            Result<LinkSteamResponse> result = await _steamService.LinkPreviewAsync(request);
            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("Auth/Link")]
        public async Task<IActionResult> Link(LinkSteamRequest request)
        {
            if (!Guid.TryParse(Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                return Challenge(JwtBearerDefaults.AuthenticationScheme);

            Result result = await _steamService.LinkSteamAsync(request, userId);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
