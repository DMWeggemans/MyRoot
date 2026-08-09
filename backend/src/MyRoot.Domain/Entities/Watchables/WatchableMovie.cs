using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Watchables
{
    public class WatchableMovie : Watchable
    {
        public TimeSpan Duration { get; set; }

        public WatchableMovie() => Type = WatchableType.Movie;
    }
}
