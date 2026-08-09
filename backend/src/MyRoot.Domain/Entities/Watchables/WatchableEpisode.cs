using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Watchables
{
    public class WatchableEpisode : Watchable
    {
        public TimeSpan Duration { get; set; }
        public int EpisodeNumber { get; set; }

        // Navigation Keys
        public Guid SeasonId { get; set; }

        // Navigation Properties
        public WatchableSeason Season { get; set; } = null!;

        public WatchableEpisode() => Type = WatchableType.Episode;
    }
}
