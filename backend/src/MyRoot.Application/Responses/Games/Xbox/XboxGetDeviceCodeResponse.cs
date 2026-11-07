using System.Text.Json.Serialization;

namespace MyRoot.Application.Responses.Games.Xbox
{
    public record struct XboxGetDeviceCodeResponse(
        [property: JsonPropertyName("user_code")] string UserCode,
        [property: JsonPropertyName("device_code")] string DeviceCode,
        [property: JsonPropertyName("verification_uri")] string VerificationUri,
        [property: JsonPropertyName("interval")] int Interval,
        [property: JsonPropertyName("expires_in")] long ExpiresIn
    ) { }
}
