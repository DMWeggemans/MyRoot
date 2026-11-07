namespace MyRoot.Application.DTOs.Games.Xbox
{
    public record struct XboxDeviceCode(
        string Code,
        string SessionId,
        string Url,
        int IntervalInSeconds,
        DateTimeOffset ExpiresOn
    ) { }
}
