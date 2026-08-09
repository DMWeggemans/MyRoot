using MyRoot.Domain.Enums;

namespace MyRoot.Domain.Entities.Abstracts
{
    public abstract class Watchable
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public WatchableType Type { get; set; }
        public int SortIndex { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
