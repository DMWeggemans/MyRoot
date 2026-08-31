namespace MyRoot.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<string> Tags { get; set; } = [];

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation Properties
        public List<Achievement> Achievements { get; set; } = [];
    }
}
