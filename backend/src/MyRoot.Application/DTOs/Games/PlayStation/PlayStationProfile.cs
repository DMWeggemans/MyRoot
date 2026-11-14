namespace MyRoot.Application.DTOs.Games.PlayStation
{
    public class PlayStationProfile
    {
        public string OnlineId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AboutMe { get; set; }
        public string PublicAvatar { get; set; } = string.Empty;
        public string? PrivateAvatar { get; set; }
        public bool HasPSPlus { get; set; }
    }
}
