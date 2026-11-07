namespace MyRoot.Application.Requests.Games.Steam
{
    public class LinkSteamRequest
    {
        public string SteamId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
