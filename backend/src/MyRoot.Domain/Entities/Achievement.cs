namespace MyRoot.Domain.Entities
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Progression { get; set; }
        public int DefaultOrderIndex { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Keys
        public Guid GameId { get; set; }

        // Navigation Properties
        public Game Game { get; set; } = null!;
    }
}
