using Microsoft.EntityFrameworkCore;
using MyRoot.Application.DTOs;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.Extensions.Auth;
using MyRoot.Application.Interfaces.Services.Auth;
using MyRoot.Application.Requests.Auth;
using MyRoot.Domain.Entities;
using MyRoot.Domain.Interfaces;
using MyRoot.Domain.Interfaces.Services;

namespace MyRoot.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService PasswordService;
        private readonly IApplicationDBContext Context;
        private readonly ITokenService TokenService;

        public AuthService(IPasswordService passwordService, IApplicationDBContext context, ITokenService tokenService)
        {
            PasswordService = passwordService;
            TokenService = tokenService;
            Context = context;
        }

        public async Task<Result<AuthTokens>> LoginAsync(LoginUserRequest request)
        {
            User? user = EmailExtensions.IsValid(request.User)
                ? await Context.Users.SingleOrDefaultAsync(u => u.Email == request.User)
                : await Context.Users.SingleOrDefaultAsync(u => u.UserName == request.User);

            if (user is null || !PasswordService.VerifyPassword(user.PasswordHash, request.Password))
                return Result<AuthTokens>.Failed("INCORRECT_CREDENTIALS");

            return Result<AuthTokens>.Success(new AuthTokens(
                TokenService.GenerateJWT(user),
                await TokenService.GenerateRefreshTokenAsync(user)
            ));
        }

        public async Task<Result<AuthTokens>> RegisterAsync(RegisterUserRequest request)
        {
            if (!EmailExtensions.IsValid(request.Email))
                return Result<AuthTokens>.Failed("INVALID_EMAIL");

            User user = new User()
            {
                PasswordHash = PasswordService.HashPassword(request.Password),
                CreatedOn = DateTime.UtcNow,
                UserName = request.UserName,
                Email = request.Email
            };

            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            return Result<AuthTokens>.Success(new AuthTokens(
                TokenService.GenerateJWT(user),
                await TokenService.GenerateRefreshTokenAsync(user)
            ));
        }

        public async Task<Result<AuthTokens>> RefreshAsync(string token)
        {
            (AuthToken? refreshToken, User? user) = await TokenService.RefreshTokenAsync(token);
            if (refreshToken is null || user is null)
                return Result<AuthTokens>.Failed("NOT_AUTHORIZED");

            return Result<AuthTokens>.Success(new AuthTokens(
                TokenService.GenerateJWT(user),
                refreshToken
            ));
        }

        public async Task LogoutAsync(string token)
            => await TokenService.DeleteRefreshTokenAsync(token);
    }
}
