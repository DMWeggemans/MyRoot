namespace MyRoot.Application.DTOs.Games.Xbox
{
    public class XboxDeviceCode
    {
        public string Code { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int IntervalInSeconds { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
    }
}
