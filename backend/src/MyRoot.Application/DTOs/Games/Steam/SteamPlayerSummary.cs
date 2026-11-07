namespace MyRoot.Application.DTOs.Games.Steam
{
    public record struct SteamPlayerSummary(
        string SteamId,
        bool IsPublic,
        string UserName,
        string ProfileUrl,
        string Avatar,
        DateTimeOffset LastLogOff,
        DateTimeOffset TimeCreated
    ) { }
}
