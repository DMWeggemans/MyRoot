using MyRoot.Application.Requests.Games.Steam;
using MyRoot.Application.Responses.Games.Steam;

namespace MyRoot.Application.Extensions.Games.Steam
{
    public static class LinkSteamResponseExtensions
    {
        public static LinkSteamRequest ToLinkPreviewRequest(this LinkSteamResponse response)
            => new LinkSteamRequest()
            {
                Key = response.Key,
                SteamId = response.SteamId
            };
    }
}
