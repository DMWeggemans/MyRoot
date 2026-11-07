using MyRoot.Application.Responses.Games.Xbox;
using MyRoot.Application.DTOs.Games.Xbox;

namespace MyRoot.Application.Extensions.Games.Xbox
{
    public static class XboxGetDeviceCodeResponseExtensions
    {
        public static XboxDeviceCode ToXboxDeviceCode(this XboxGetDeviceCodeResponse response)
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
