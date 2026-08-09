using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Watchables
{
    public class WatchableSeason : Watchable
    {
        public int? SeasonNumber { get; set; }

        // Navigation Keys
        public Guid ShowId { get; set; }

        // Navigation Properties
        public List<WatchableEpisode> Episodes { get; set; } = [];
        public WatchableShow Show { get; set; } = null!;

        public WatchableSeason() => Type = WatchableType.Season;
    }
}
