using MyRoot.Application.Interfaces.Clients.Games;
using Microsoft.AspNetCore.Mvc;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayStationController(IPlayStationClient _playStationClient) : ControllerBase
    {
        [HttpGet("Code/{npsso}")]
        public async Task<IActionResult> GetCodeByNpsso(string npsso)
        {
            return Ok(await _playStationClient.GetCodeByNpssoAsync(npsso));
        }

        [HttpGet("Token/{code}")]
        public async Task<IActionResult> GetTokensByCode(string code)
        {
            return Ok(await _playStationClient.GetTokensByCodeAsync(code));
        }

        [HttpGet("Refresh/{refreshToken}")]
        public async Task<IActionResult> RefreshTokens(string refreshToken)
        {
            return Ok(await _playStationClient.RefreshTokensAsync(refreshToken));
        }

        [HttpGet("User/Id/{userName}")]
        public async Task<IActionResult> GetAccountIdByUserName(string userName, string accessToken)
        {
            return Ok(await _playStationClient.GetAccountIdByUserNameAsync(accessToken, userName));
        }

        [HttpGet("User/{accountId}")]
        public async Task<IActionResult> GetProfileByAccountId(string accountId, string accessToken)
        {
            return Ok(await _playStationClient.GetProfileByAccountIdAsync(accessToken, accountId));
        }
    }
}
