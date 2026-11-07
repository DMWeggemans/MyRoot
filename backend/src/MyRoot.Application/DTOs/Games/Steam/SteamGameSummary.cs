namespace MyRoot.Application.DTOs.Games.Steam
{
    public record struct SteamGameSummary(
        int AppId,
        string Name,
        int PlaytimeInMinutes,
        DateTimeOffset LastPlayed
    ) { }
}
