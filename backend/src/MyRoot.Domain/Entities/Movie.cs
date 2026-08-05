using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities
{
    public class Movie : Watchable
    {
        public TimeSpan Duration { get; set; }

        public Movie()
        {
            Type = WatchableType.Movie;
        }
    }
}
