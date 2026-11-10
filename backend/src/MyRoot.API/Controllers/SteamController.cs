using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Requests.Games.Steam;
using System.Security.Claims;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SteamController(ISteamService _steamService) : ControllerBase
    {
        [HttpGet("SteamId")]
        public async Task<IActionResult> GetSteamId()
        {
            OpenIdParameters parameters = OpenIdParameters.Build(Request.Query);
            Result<string> result = await _steamService.GetSteamIdAsync(parameters);
            return Content(result.Value!, "text/html");
        }

        //[HttpPost("Link")]
        //public async Task<IActionResult> Link(LinkSteamRequest request)
        //{
        //    if (!Guid.TryParse(Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
        //        return Challenge(JwtBearerDefaults.AuthenticationScheme);

        //    Result result = await _steamService.LinkSteamAsync(request, userId);
        //    if (!result.Succeeded)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
    }
}
