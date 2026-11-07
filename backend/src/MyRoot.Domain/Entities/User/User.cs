using MyRoot.Domain.Interfaces.Entities;

namespace MyRoot.Domain.Entities.User
{
    public class User : IHasGuid
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string NormalizedUserName { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        // Navigation Properties
        public List<RefreshToken> RefreshTokens { get; set; } = [];
        //public List<Account> Accounts { get; set; } = [];
    }
}
