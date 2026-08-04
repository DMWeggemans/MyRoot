using Microsoft.AspNetCore.Identity;
using MyRoot.Domain.Interfaces.Services;

namespace MyRoot.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> PasswordHasher = new PasswordHasher<object>();
        private readonly object User = new object();

        public string HashPassword(string password)
            => PasswordHasher.HashPassword(User, password);

        public bool VerifyPassword(string hash, string password)
        {
            PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(User, hash, password);
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
