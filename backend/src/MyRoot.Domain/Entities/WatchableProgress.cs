using MyRoot.Domain.Entities.Abstracts;

namespace MyRoot.Domain.Entities
{
    public class WatchableProgress
    {
        public Guid Id { get; set; }
        public TimeSpan Progress { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid UserId { get; set; }
        public Guid WatchableId { get; set; }
        public Guid? EpisodeId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Watchable Watchable { get; set; } = null!;
        public Episode? Episode { get; set; }
    }
}
