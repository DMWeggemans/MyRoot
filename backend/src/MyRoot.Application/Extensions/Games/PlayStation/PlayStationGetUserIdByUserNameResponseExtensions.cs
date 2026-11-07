using MyRoot.Application.DTOs.Games.PlayStation;
using MyRoot.Application.Responses.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class PlayStationGetUserIdByUserNameResponseExtensions
    {
        public static PlayStationUserIds ToPlayStationUserIds(this PlayStationGetUserIdByUserNameResponse response)
            => new PlayStationUserIds()
            {
                OnlineId = response.Profile.OnlineId,
                AccountId = response.Profile.AccountId,
                CurrentOnlineId = response.Profile.CurrentOnlineId ?? response.Profile.OnlineId
            };
    }
}
