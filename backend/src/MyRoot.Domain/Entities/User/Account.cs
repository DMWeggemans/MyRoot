using MyRoot.Domain.Interfaces.Entities;
using MyRoot.Domain.Entities.Game;
using MyRoot.Domain.Enums.User;

namespace MyRoot.Domain.Entities.User
{
    public class Account : IHasGuid
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid UserId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public List<GameProvider> GameProviders { get; set; } = [];
    }
}
