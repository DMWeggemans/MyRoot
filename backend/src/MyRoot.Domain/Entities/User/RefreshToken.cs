using MyRoot.Domain.Interfaces.Entities;

namespace MyRoot.Domain.Entities.User
{
    public class RefreshToken : IHasGuid
    {
        public Guid Id { get; set; }
        public string TokenHash { get; set; } = string.Empty;

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }

        // Navigation Keys
        public Guid UserId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
    }
}
