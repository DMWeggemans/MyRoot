using Response = MyRoot.Domain.Responses.Games.PlayStation.PlayStation;
using MyRoot.Application.DTOs.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class GetProfileByAccountIdResponseExtensions
    {
        private readonly static List<string> OrderPreferences = ["xl", "l", "m", "s"];

        public static PlayStationProfile ToPlayStationProfile(this Response.GetProfileByAccountIdResponse response)
            => new PlayStationProfile()
            {
                OnlineId = response.OnlineId,
                DisplayName = response.PersonalDetail.DisplayName ?? response.OnlineId,
                FirstName = response.PersonalDetail.FirstName,
                LastName = response.PersonalDetail.LastName,
                AboutMe = response.AboutMe.Length > 0 ? response.AboutMe : null,
                PublicAvatar = response.Avatars.OrderBy(x=> OrderPreferences.IndexOf(x.Size)).FirstOrDefault()?.Url ?? string.Empty,
                PrivateAvatar = response.PersonalDetail.ProfilePictures?.OrderBy(x => OrderPreferences.IndexOf(x.Size)).FirstOrDefault()?.Url,
                HasPSPlus = response.IsPlus
            };
    }
}
