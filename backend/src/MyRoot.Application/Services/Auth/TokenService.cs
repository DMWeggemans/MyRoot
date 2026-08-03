using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.Extensions;
using MyRoot.Application.Interfaces.Services.Auth;
using MyRoot.Domain.Entities;
using MyRoot.Domain.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyRoot.Application.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IApplicationDBContext Context;
        private readonly IConfiguration Configuration;

        public TokenService(IApplicationDBContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        public AuthToken GenerateJWT(User user)
        {
            ClaimsIdentity claims = new ClaimsIdentity([
               new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
           ]);

            byte[] secret = Encoding.UTF8.GetBytes(Configuration.GetValueOrThrow("JWT:Secret"));

            SymmetricSecurityKey key = new SymmetricSecurityKey(secret);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expiresOn = GetExpiresOn();

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Issuer = Configuration.GetValueOrThrow("JWT:Issuer"),
                Audience = Configuration.GetValueOrThrow("JWT:Audience"),
                SigningCredentials = credentials,
                Expires = expiresOn,
                Subject = claims
            };

            string token = new JsonWebTokenHandler().CreateToken(descriptor);
            return new AuthToken(token, expiresOn);
        }

        public async Task<AuthToken> GenerateRefreshTokenAsync(User user)
        {
            DateTime expiresOn = GetExpiresOn();
            string token = GenerateToken();

            Context.RefreshTokens.Add(new RefreshToken()
            {
                LoggedInOn = DateTime.UtcNow,
                TokenHash = Hash(token),
                ExpiresOn = expiresOn,
                UserId = user.Id
            });

            await Context.SaveChangesAsync();

            return new AuthToken(token, expiresOn);
        }

        public async Task<(AuthToken?, User?)> RefreshTokenAsync(string token)
        {
            RefreshToken? refreshToken = await Context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.TokenHash == Hash(token));

            if (refreshToken is null || refreshToken.ExpiresOn <= DateTime.UtcNow)
            {
                if (refreshToken is not null)
                {
                    Context.RefreshTokens.Remove(refreshToken);
                    await Context.SaveChangesAsync();
                }

                return (null, null);
            }

            DateTime expiresOn = GetExpiresOn();
            token = GenerateToken();
            
            refreshToken.TokenHash = Hash(token);
            refreshToken.ExpiresOn = expiresOn;
            await Context.SaveChangesAsync();

            return (new AuthToken(token, expiresOn), refreshToken.User);
        }

        public async Task DeleteRefreshTokenAsync(string token)
            => await Context.RefreshTokens.Where(rt => rt.TokenHash == Hash(token))
                .ExecuteDeleteAsync();

        private DateTime GetExpiresOn()
        {
            if (!int.TryParse(Configuration.GetValueOrThrow("RefreshToken:ExpiryMinutes"), out int expiryMinutes))
                Configuration.Throw("RefreshToken:ExpiryMinutes");
            return DateTime.UtcNow.AddMinutes(expiryMinutes);
        }

        private static string Hash(string value)
            => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

        private static string GenerateToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
