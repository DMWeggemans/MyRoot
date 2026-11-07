using MyRoot.Domain.Interfaces.Entities;
using MyRoot.Domain.Enums.Game;

namespace MyRoot.Domain.Entities.Game
{
    public class GameAchievement : IHasGuid, IHasOrder
    {
        public Guid Id { get; set; }
        public int Index { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public GameAchievementRarity Rarity { get; set; }
        public int Progression { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }

        // Navigation Keys
        public Guid GameId { get; set; }

        // Navigation Properties
        public Game Game { get; set; } = null!;
    }
}
