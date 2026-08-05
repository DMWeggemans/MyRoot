namespace MyRoot.Domain.Entities
{
    public class Episode
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int EpisodeNumber { get; set; }
        public TimeSpan Duration { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid SeasonId { get; set; }

        // Navigation Properties
        public Season Season { get; set; } = null!;
        public List<WatchableProgress> WatchableProgresses { get; set; } = [];
    }
}
