namespace MyRoot.Application.Requests.Games.Steam
{
    public class LinkSteamRequest
    {
        public Guid Key { get; set; }
        public string SteamId { get; set; } = string.Empty;
    }
}
