using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Watchables
{
    public class WatchableCollection : Watchable
    {
        // Navigation Keys
        public Guid? CollectionId { get; set; }

        // Navigation Properties
        public List<Watchable> Watchables { get; set; } = [];
        public WatchableCollection? Collection { get; set; }

        public WatchableCollection() => Type = WatchableType.Collection;
    }
}
