using MyRoot.Application.DTOs;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.Requests.Auth;

namespace MyRoot.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthTokens>> LoginAsync(LoginUserRequest request);
        Task<Result<AuthTokens>> RegisterAsync(RegisterUserRequest request);
        Task<Result<AuthTokens>> RefreshAsync(string token);
        Task LogoutAsync(string token);
    }
}
