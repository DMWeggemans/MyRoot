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
                ? await Context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == request.User.ToLower())
                : await Context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == request.User.ToLower());

            if (user is null || !PasswordService.VerifyPassword(user.PasswordHash, request.Password))
                return Result<AuthTokens>.Failed("INCORRECT_CREDENTIALS");

            return Result<AuthTokens>.Success(new AuthTokens(
                TokenService.GenerateJWT(user),
                await TokenService.GenerateRefreshTokenAsync(user)
            ));
        }

        public async Task<Result<AuthTokens>> RegisterAsync(RegisterUserRequest request)
        {
            if (request.UserName.Length < 4 || request.UserName.Length > 32)
                return Result<AuthTokens>.Failed("INVALID_USERNAME");

            if (request.Password.Length < 8 || request.Password.Length > 128)
                return Result<AuthTokens>.Failed("INVALID_PASSWORD");

            if (await Context.Users.AnyAsync(u => u.UserName.ToLower() == request.UserName.ToLower()))
                return Result<AuthTokens>.Failed("USERNAME_EXISTS");

            if (!EmailExtensions.IsValid(request.Email))
                return Result<AuthTokens>.Failed("INVALID_EMAIL");

            if (await Context.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower()))
                return Result<AuthTokens>.Failed("EMAIL_EXISTS");

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
