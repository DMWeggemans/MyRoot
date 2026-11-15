namespace MyRoot.Application.Responses.Games.PlayStation
{
    public class LinkPlayStationResponse
    {
        public Guid Key { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public int TotalGames { get; set; }
        public bool HasPSPlus { get; set; }
        public DateTimeOffset? LastEarned { get; set; }
    }
}
