namespace MyRoot.Application.DTOs.Games.PlayStation
{
    public record struct PlayStationUserIds(
        string OnlineId,
        string AccountId,
        string CurrentOnlineId
    ) { }
}
