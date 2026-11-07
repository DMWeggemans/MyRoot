using MyRoot.Domain.Interfaces.Entities;
using MyRoot.Domain.Entities.User;

namespace MyRoot.Domain.Entities.Game
{
    public class GameProvider : IHasGuid
    {
        public Guid Id { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public DateTimeOffset LinkedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid AccountId { get; set; }

        // Navigation Properties
        public Account Account { get; set; } = null!;
    }
}
