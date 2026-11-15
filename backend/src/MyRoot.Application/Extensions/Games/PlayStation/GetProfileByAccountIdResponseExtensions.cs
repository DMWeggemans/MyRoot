using Response = MyRoot.Domain.Responses.Games.PlayStation.PlayStation;
using MyRoot.Application.Responses.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class GetProfileByAccountIdResponseExtensions
    {
        private readonly static List<string> OrderPreferences = ["xl", "l", "m", "s"];

        public static LinkPlayStationResponse ToPlayStationProfile(this Response.GetProfileByAccountIdResponse response, Guid key, string accountId, List<Response.GetTrophyTitlesByAccountIdResponse.Payload> games, bool hasGamesPrivate)
            => new LinkPlayStationResponse()
            {
                Key = key,
                AccountId = accountId,
                DisplayName = response.PersonalDetail.DisplayName ?? response.OnlineId,
                IsPublic = !hasGamesPrivate && (response.Languages.Count > 0 || !string.IsNullOrWhiteSpace(response.AboutMe)),
                FullName = !string.IsNullOrWhiteSpace(response.PersonalDetail.FirstName) && !string.IsNullOrWhiteSpace(response.PersonalDetail.LastName)
                    ? $"{response.PersonalDetail.FirstName} {response.PersonalDetail.LastName}" : null,
                Avatar = response.PersonalDetail.ProfilePictures?.OrderBy(x => OrderPreferences.IndexOf(x.Size)).FirstOrDefault()?.Url
                    ?? response.Avatars.OrderBy(x=> OrderPreferences.IndexOf(x.Size)).FirstOrDefault()?.Url,
                TotalGames = games.Count,
                HasPSPlus = response.IsPlus,
                LastEarned = games.OrderByDescending(x => x.LastUpdatedDateTime).FirstOrDefault()?.LastUpdatedDateTime
            };
    }
}
