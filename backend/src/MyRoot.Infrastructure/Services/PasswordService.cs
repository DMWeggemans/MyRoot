using MyRoot.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MyRoot.Infrastructure.Services
{
    public class PasswordService(ILogger<PasswordService> _logger) : IPasswordService
    {
        private readonly PasswordHasher<object> PasswordHasher = new PasswordHasher<object>();
        private readonly object User = new object();

        public string HashPassword(string password)
            => PasswordHasher.HashPassword(User, password);

        public bool VerifyPassword(string hash, string password)
        {
            PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(User, hash, password);
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                _logger.LogWarning("A password that needs to be rehashed has been found!");
                return true;
            }

            return result == PasswordVerificationResult.Success;
        }
    }
}
