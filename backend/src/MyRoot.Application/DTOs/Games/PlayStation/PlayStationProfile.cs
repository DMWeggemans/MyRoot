namespace MyRoot.Application.DTOs.Games.PlayStation
{
    public record struct PlayStationProfile(
        string OnlineId,
        string DisplayName,
        string? FirstName,
        string? LastName,
        string? AboutMe,
        string PublicAvatar,
        string? PrivateAvatar,
        bool HasPSPlus
    ) { }
}
