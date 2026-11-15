using MyRoot.Application.Responses.Games.PlayStation;
using MyRoot.Application.Interfaces.Services.Games;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayStationController(IPlayStationService _playStationService) : ControllerBase
    {
        [HttpGet("User/OnlineId")]
        public async Task<IActionResult> LinkUserByOnlineId([FromQuery] string onlineId)
        {
            if (onlineId == "me")
                return StatusCode(403, Result.Failed("INVALID_ID"));

            Result<LinkPlayStationResponse> result = await _playStationService.GetProfileByOnlineIdAsync(onlineId);
            if (!result.Succeeded)
            {
                if (new List<string?>() { "USER_NOT_FOUND" }.Contains(result.Message))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("User/NPSSO")]
        public async Task<IActionResult> LinkUserByNPSSO([FromQuery] string npsso)
        {
            Result<LinkPlayStationResponse> result = await _playStationService.GetProfileByNpssoAsync(npsso);
            if (!result.Succeeded)
            {
                if (new List<string?>() { "USER_NOT_FOUND" }.Contains(result.Message))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
