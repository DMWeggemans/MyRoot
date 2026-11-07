using MyRoot.Application.Interfaces.Clients.Games;
using Microsoft.AspNetCore.Mvc;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XboxController(IXboxClient _xboxClient) : ControllerBase
    {
        [HttpGet("Device/Code")]
        public async Task<IActionResult> GetDeviceCode()
        {
            return Ok(await _xboxClient.GetDeviceCodeAsync());
        }

        [HttpGet("Device/Check/{deviceCode}")]
        public async Task<IActionResult> CheckDeviceCode(string deviceCode)
        {
            return Ok(await _xboxClient.CheckDeviceCodeAsync(deviceCode));
        }

        [HttpGet("Refresh/{refreshToken}")]
        public async Task<IActionResult> RefreshTokens(string refreshToken)
        {
            return Ok(await _xboxClient.RefreshTokensAsync(refreshToken));
        }
    }
}
