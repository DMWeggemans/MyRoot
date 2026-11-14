using Response = MyRoot.Domain.Responses.Games.PlayStation.PlayStation;
using MyRoot.Application.DTOs.Games.PlayStation;

namespace MyRoot.Application.Extensions.Games.PlayStation
{
    public static class GetUserIdByUserNameResponseExtensions
    {
        public static PlayStationUserIds ToPlayStationUserIds(this Response.GetAccountIdByUserNameResponse response)
            => new PlayStationUserIds()
            {
                OnlineId = response.Profile.OnlineId,
                AccountId = response.Profile.AccountId,
                CurrentOnlineId = response.Profile.CurrentOnlineId ?? response.Profile.OnlineId
            };
    }
}
