using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.Xbox
{
    public static partial class Xbox
    {
        public class GetDeviceCodeResponse
        {
            [JsonPropertyName("user_code")] public string UserCode { get; set; } = string.Empty;
            [JsonPropertyName("device_code")] public string DeviceCode { get; set; } = string.Empty;
            [JsonPropertyName("verification_uri")] public string VerificationUri { get; set; } = string.Empty;
            [JsonPropertyName("interval")] public int Interval { get; set; }
            [JsonPropertyName("expires_in")] public long ExpiresIn { get; set; }
        }
    }
}
