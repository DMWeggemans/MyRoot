namespace MyRoot.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Properties
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
