using MyRoot.Application.Interfaces.Services.Auth;
using MyRoot.Application.Extensions.Auth;
using MyRoot.Domain.Interfaces.Services;
using MyRoot.Application.Requests.Auth;
using Microsoft.EntityFrameworkCore;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Domain.Interfaces;
using MyRoot.Application.DTOs;
using MyRoot.Domain.Entities;

namespace MyRoot.Application.Services.Auth
{
    public class AuthService(IPasswordService _passwordService, IApplicationDBContext _context, ITokenService _tokenService) : IAuthService
    {
        public async Task<Result<AuthTokens>> LoginAsync(LoginUserRequest request)
        {
            bool isValidEmail = EmailExtensions.IsValid(request.User);
            User? user = isValidEmail
                ? await _context.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == request.User.ToUpperInvariant())
                : await _context.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == request.User.ToUpperInvariant());

            if (user is null || !_passwordService.VerifyPassword(user.PasswordHash, request.Password))
            {
                if (!isValidEmail)
                    return Result<AuthTokens>.Failed("INCORRECT_CREDENTIALS");

                user = await _context.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == request.User.ToUpperInvariant());
                if (user is null || !_passwordService.VerifyPassword(user.PasswordHash, request.Password))
                    return Result<AuthTokens>.Failed("INCORRECT_CREDENTIALS");
            }

            return Result<AuthTokens>.Success(new AuthTokens(
                _tokenService.GenerateJWT(user),
                await _tokenService.GenerateRefreshTokenAsync(user)
            ));
        }

        public async Task<Result<AuthTokens>> RegisterAsync(RegisterUserRequest request)
        {
            if (request.UserName.Length < 4 || request.UserName.Length > 32)
                return Result<AuthTokens>.Failed("INVALID_USERNAME");

            if (request.Password.Length < 8 || request.Password.Length > 128)
                return Result<AuthTokens>.Failed("INVALID_PASSWORD");

            if (await _context.Users.AnyAsync(u => u.NormalizedUserName == request.UserName.ToUpperInvariant()))
                return Result<AuthTokens>.Failed("USERNAME_EXISTS");

            if (!EmailExtensions.IsValid(request.Email))
                return Result<AuthTokens>.Failed("INVALID_EMAIL");

            if (request.Email is not null && await _context.Users.AnyAsync(u => u.NormalizedEmail == request.Email.ToUpperInvariant()))
                return Result<AuthTokens>.Failed("EMAIL_EXISTS");

            User user = new User()
            {
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpperInvariant(),

                Email = request.Email,
                NormalizedEmail = request.Email?.ToUpperInvariant(),

                PasswordHash = _passwordService.HashPassword(request.Password),
                CreatedOn = DateTime.UtcNow,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Result<AuthTokens>.Success(new AuthTokens(
                _tokenService.GenerateJWT(user),
                await _tokenService.GenerateRefreshTokenAsync(user)
            ));
        }

        public async Task<Result<AuthTokens>> RefreshAsync(string token)
        {
            (AuthToken? refreshToken, User? user) = await _tokenService.RefreshTokenAsync(token);
            if (refreshToken is null || user is null)
                return Result<AuthTokens>.Failed();

            return Result<AuthTokens>.Success(new AuthTokens(
                _tokenService.GenerateJWT(user),
                refreshToken
            ));
        }

        public async Task LogoutAsync(string token)
            => await _tokenService.DeleteRefreshTokenAsync(token);
    }
}
