using MyRoot.Application.Interfaces.Services.Auth;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using MyRoot.Application.Extensions;
using MyRoot.Application.DTOs.Auth;
using System.Security.Cryptography;
using MyRoot.Domain.Interfaces;
using MyRoot.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace MyRoot.Application.Services.Auth
{
    public class TokenService(IApplicationDBContext _context, IConfiguration _configuration) : ITokenService
    {
        public AuthToken GenerateJWT(User user)
        {
            ClaimsIdentity claims = new ClaimsIdentity([
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
           ]);

            byte[] secret = Encoding.UTF8.GetBytes(_configuration.GetValueOrThrow("JWT:Secret"));

            SymmetricSecurityKey key = new SymmetricSecurityKey(secret);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expiresOn = GetExpiresOn("JWT:ExpiryMinutes");

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration.GetValueOrThrow("JWT:Issuer"),
                Audience = _configuration.GetValueOrThrow("JWT:Audience"),
                SigningCredentials = credentials,
                Expires = expiresOn,
                Subject = claims
            };

            string token = new JsonWebTokenHandler().CreateToken(descriptor);
            return new AuthToken(token, expiresOn);
        }

        public async Task<AuthToken> GenerateRefreshTokenAsync(User user)
        {
            DateTime expiresOn = GetExpiresOn("RefreshToken:ExpiryMinutes");
            string token = GenerateToken();

            _context.RefreshTokens.Add(new RefreshToken()
            {
                TokenHash = Hash(token),

                CreatedOn = DateTime.UtcNow,
                ExpiresOn = expiresOn,

                UserId = user.Id
            });

            await _context.SaveChangesAsync();

            return new AuthToken(token, expiresOn);
        }

        public async Task<(AuthToken?, User?)> RefreshTokenAsync(string token)
        {
            RefreshToken? refreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.TokenHash == Hash(token));

            if (refreshToken is null || refreshToken.ExpiresOn <= DateTime.UtcNow)
            {
                if (refreshToken is not null)
                {
                    _context.RefreshTokens.Remove(refreshToken);
                    await _context.SaveChangesAsync();
                }

                return (null, null);
            }

            DateTime expiresOn = GetExpiresOn("RefreshToken:ExpiryMinutes");
            token = GenerateToken();
            
            refreshToken.TokenHash = Hash(token);
            refreshToken.ExpiresOn = expiresOn;
            await _context.SaveChangesAsync();

            return (new AuthToken(token, expiresOn), refreshToken.User);
        }

        public async Task DeleteRefreshTokenAsync(string token)
            => await _context.RefreshTokens.Where(rt => rt.TokenHash == Hash(token))
                .ExecuteDeleteAsync();

        private DateTime GetExpiresOn(string key)
        {
            if (!int.TryParse(_configuration.GetValueOrThrow(key), out int expiryMinutes))
                _configuration.Throw(key);
            return DateTime.UtcNow.AddMinutes(expiryMinutes);
        }

        private static string Hash(string value)
            => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

        private static string GenerateToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
