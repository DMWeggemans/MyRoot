namespace MyRoot.Domain.Entities
{
    public class Season
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int SeasonNumber { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid ShowId { get; set; }

        // Navigation Properties
        public Show Show { get; set; } = null!;
        public List<Episode> Episodes { get; set; } = [];
    }
}
