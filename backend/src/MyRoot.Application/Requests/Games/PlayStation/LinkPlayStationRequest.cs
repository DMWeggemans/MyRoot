namespace MyRoot.Application.Requests.Games.PlayStation
{
    public class LinkPlayStationRequest
    {
        public Guid Key { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
