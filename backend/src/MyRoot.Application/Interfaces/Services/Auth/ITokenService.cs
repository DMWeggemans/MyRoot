using MyRoot.Application.DTOs.Auth;
using MyRoot.Domain.Entities.Auth;

namespace MyRoot.Application.Interfaces.Services.Auth
{
    public interface ITokenService
    {
        AuthToken GenerateJWT(User user);
        Task<AuthToken> GenerateRefreshTokenAsync(User user);
        Task<(AuthToken?, User?)> RefreshTokenAsync(string token);
        Task DeleteRefreshTokenAsync(string token);
    }
}
