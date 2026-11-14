namespace MyRoot.Application.DTOs.Games.Steam
{
    public class SteamGameSummary
    {
        public int AppId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PlaytimeInMinutes { get; set; }
        public DateTimeOffset LastPlayed { get; set; }
    }
}
