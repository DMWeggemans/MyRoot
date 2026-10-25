using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using MyRoot.Application.Requests.Auth;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Domain.Configurations;
using Microsoft.AspNetCore.Mvc;
using MyRoot.Application.DTOs;
using System.Security.Claims;

namespace MyRoot.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IWebHostEnvironment _environment, IAuthService _authService) : ControllerBase
    {
        private readonly CookieOptions CookieOptions = new CookieOptions()
        {
            Domain = _environment.IsProduction() ? ApplicationConfiguration.Urls.Application : null,
            Secure = _environment.IsProduction(),
            SameSite = SameSiteMode.Lax,
            HttpOnly = true
        };

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            Result<AuthTokens> result = await _authService.LoginAsync(request);
            if (!result.Succeeded)
            {
                if (new List<string?>() { "INCORRECT_CREDENTIALS" }.Contains(result.Message))
                    return Unauthorized(result);

                return BadRequest(result);
            }

            HandleAuthTokens(result);
            result.Value = null;

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            Result<AuthTokens> result = await _authService.RegisterAsync(request);
            if (!result.Succeeded)
                return BadRequest(result);

            HandleAuthTokens(result);
            result.Value = null;

            return Ok(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            string? token = Request.Cookies[ApplicationConfiguration.Auth.Tokens.RefreshToken];
            if (string.IsNullOrEmpty(token))
            {
                Response.Cookies.Delete(ApplicationConfiguration.Auth.Tokens.AccessToken, CookieOptions);
                return Challenge(JwtBearerDefaults.AuthenticationScheme);
            }

            Result<AuthTokens> result = await _authService.RefreshAsync(token);
            if (!result.Succeeded)
            {
                Response.Cookies.Delete(ApplicationConfiguration.Auth.Tokens.AccessToken, CookieOptions);
                return Challenge(JwtBearerDefaults.AuthenticationScheme);
            }

            HandleAuthTokens(result);
            result.Value = null;

            return Ok(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? token = Request.Cookies[ApplicationConfiguration.Auth.Tokens.RefreshToken];

            Response.Cookies.Delete(ApplicationConfiguration.Auth.Tokens.AccessToken, CookieOptions);
            Response.Cookies.Delete(ApplicationConfiguration.Auth.Tokens.RefreshToken, CookieOptions);

            if (token is not null)
            {
                await _authService.LogoutAsync(token);
            }

            return Ok(Result.Success());
        }

        [HttpGet("Authorize")]
        [Authorize]
        public IActionResult Authorize()
        {
            AuthorizedValues values = new AuthorizedValues();

            if (!Guid.TryParse(Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id))
                return Challenge(JwtBearerDefaults.AuthenticationScheme);
            values.Id = id;

            if (!long.TryParse(Request.HttpContext.User.FindFirstValue("exp"), out long expiresOn))
                return Challenge(JwtBearerDefaults.AuthenticationScheme);
            values.ExpiresOn = DateTimeOffset.FromUnixTimeSeconds(expiresOn).UtcDateTime;

            return Ok(Result.Success(values));
        }

        [SwaggerIgnore]
        private void HandleAuthTokens(Result<AuthTokens> result)
        {
            if (result.Value is null)
                return;
            
            CookieOptions.Expires = result.Value.JWT.ExpiresOn;
            Response.Cookies.Append(ApplicationConfiguration.Auth.Tokens.AccessToken, result.Value.JWT.Value, CookieOptions);

            CookieOptions.Expires = result.Value.RefreshToken.ExpiresOn;
            Response.Cookies.Append(ApplicationConfiguration.Auth.Tokens.RefreshToken, result.Value.RefreshToken.Value, CookieOptions);
        }
    }
}
