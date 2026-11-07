using MyRoot.Domain.Interfaces.Entities;

namespace MyRoot.Domain.Entities.Game
{
    public class Game : IHasGuid
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }

        // Navigation Properties
        public List<GameAchievement> Achievements { get; set; } = [];
    }
}
