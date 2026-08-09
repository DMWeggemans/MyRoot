using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Watchables
{
    public class WatchableShow : Watchable
    {
        // Navigation Properties
        public List<WatchableSeason> Seasons { get; set; } = [];

        public WatchableShow() => Type = WatchableType.Show;
    }
}
