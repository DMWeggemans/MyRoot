using MyRoot.Application.Responses.Games.PlayStation;
using MyRoot.Application.DTOs.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class PlayStationGetProfileByAccountIdResponseExtensions
    {
        public static List<string> OrderPreferences = ["xl", "l", "m", "s"];

        public static PlayStationProfile ToPlayStationProfile(this PlayStationGetProfileByAccountIdResponse response)
            => new PlayStationProfile()
            {
                OnlineId = response.OnlineId,
                DisplayName = response.PersonalDetail.DisplayName ?? response.OnlineId,
                FirstName = response.PersonalDetail.FirstName,
                LastName = response.PersonalDetail.LastName,
                AboutMe = response.AboutMe.Length > 0 ? response.AboutMe : null,
                PublicAvatar = response.Avatars.OrderBy(x => OrderPreferences.IndexOf(x.Size)).FirstOrDefault().Url,
                PrivateAvatar = response.PersonalDetail.ProfilePictures?.OrderBy(x => OrderPreferences.IndexOf(x.Size)).FirstOrDefault().Url,
                HasPSPlus = response.IsPlus
            };
    }
}
