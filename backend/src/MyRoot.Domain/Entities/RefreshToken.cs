namespace MyRoot.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string TokenHash { get; set; } = string.Empty;

        public DateTime LoggedInOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        // Navigation Keys
        public Guid UserId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
    }
}
