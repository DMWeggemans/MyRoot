using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using MyRoot.Application.Requests.Auth;
using MyRoot.Application.Extensions;
using MyRoot.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;
using System.Security.Claims;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CookieOptions CookieOptions;
        private readonly IAuthService AuthService;

        public AuthController(IWebHostEnvironment environment, IConfiguration configuration, IAuthService authService)
        {
            AuthService = authService;

            CookieOptions = new CookieOptions()
            {
                Domain = environment.IsProduction() ? configuration.GetValueOrThrow("UI:Url").Split('/').Last() : null,
                Secure = environment.IsProduction(),
                SameSite = SameSiteMode.Lax,
                HttpOnly = true
            };
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            Result<AuthTokens> result = await AuthService.LoginAsync(request);
            if (!result.Succeeded)
            {
                if (new List<string?>() { "INCORRECT_CREDENTIALS" }.Contains(result.Message))
                    return Unauthorized(result);

                return BadRequest(result);
            }

            if (result.Value != null)
            {
                CookieOptions.Expires = result.Value.JWT.ExpiresOn;
                Response.Cookies.Append("X-Access-Token", result.Value.JWT.Value, CookieOptions);

                CookieOptions.Expires = result.Value.RefreshToken.ExpiresOn;
                Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken.Value, CookieOptions);
            }

            result.Value = null;
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            Result<AuthTokens> result = await AuthService.RegisterAsync(request);
            if (!result.Succeeded)
                return BadRequest(result);

            if (result.Value != null)
            {
                CookieOptions.Expires = result.Value.JWT.ExpiresOn;
                Response.Cookies.Append("X-Access-Token", result.Value.JWT.Value, CookieOptions);

                CookieOptions.Expires = result.Value.RefreshToken.ExpiresOn;
                Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken.Value, CookieOptions);
            }

            result.Value = null;
            return Ok(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            string? token = Request.Cookies["X-Refresh-Token"];
            if (string.IsNullOrEmpty(token))
            {
                Response.Cookies.Delete("X-Access-Token", CookieOptions);
                return Unauthorized(Result.Failed("NOT_AUTHORIZED"));
            }

            Result<AuthTokens> result = await AuthService.RefreshAsync(token);
            if (!result.Succeeded)
            {
                if (new List<string?>() { "NOT_AUTHORIZED" }.Contains(result.Message))
                {
                    Response.Cookies.Delete("X-Access-Token", CookieOptions);
                    return Unauthorized(result);
                }

                return BadRequest(result);
            }

            if (result.Value != null)
            {
                CookieOptions.Expires = result.Value.JWT.ExpiresOn;
                Response.Cookies.Append("X-Access-Token", result.Value.JWT.Value, CookieOptions);

                CookieOptions.Expires = result.Value.RefreshToken.ExpiresOn;
                Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken.Value, CookieOptions);
            }

            result.Value = null;
            return Ok(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? token = Request.Cookies["X-Refresh-Token"];

            Response.Cookies.Delete("X-Access-Token", CookieOptions);
            Response.Cookies.Delete("X-Refresh-Token", CookieOptions);

            if (token is not null)
                await AuthService.LogoutAsync(token);

            return Ok(Result.Success("SUCCESS_LOGOUT"));
        }

        [HttpGet("Authorize")]
        [Authorize]
        public IActionResult Authorize()
        {
            AuthorizedValues values = new AuthorizedValues();

            if (!Guid.TryParse(Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id))
                return Unauthorized(Result.Failed("NOT_AUTHORIZED"));
            values.Id = id;

            if (!long.TryParse(Request.HttpContext.User.FindFirstValue("exp"), out long expiresOn))
                return Unauthorized(Result.Failed("NOT_AUTHORIZED"));
            values.ExpiresOn = DateTimeOffset.FromUnixTimeSeconds(expiresOn).UtcDateTime;

            return Ok(Result.Success(values));
        }
    }
}
