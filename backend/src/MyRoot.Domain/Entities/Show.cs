using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities
{
    public class Show : Watchable
    {
        // Navigation Properties
        public List<Season> Seasons { get; set; } = [];

        public Show()
        {
            Type = WatchableType.Show;
        }
    }
}
