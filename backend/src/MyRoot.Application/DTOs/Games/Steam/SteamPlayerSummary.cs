namespace MyRoot.Application.DTOs.Games.Steam
{
    public class SteamPlayerSummary
    {
        public string SteamId { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public DateTimeOffset LastLogOff { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
    }
}
