using Response = MyRoot.Domain.Responses.Games.Xbox.Xbox;
using MyRoot.Application.DTOs.Games.Xbox;

namespace MyRoot.Application.Extensions.Games.Xbox
{
    public static class GetDeviceCodeResponseExtensions
    {
        public static XboxDeviceCode ToXboxDeviceCode(this Response.GetDeviceCodeResponse response)
            => new XboxDeviceCode()
            {
                Code = response.UserCode,
                SessionId = response.DeviceCode,
                Url = response.VerificationUri,
                IntervalInSeconds = response.Interval,
                ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn)
            };
    }
}
