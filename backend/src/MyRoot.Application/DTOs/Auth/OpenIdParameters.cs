using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Text.Json.Serialization;

namespace MyRoot.Application.DTOs.Auth
{
    public class OpenIdParameters
    {
        [JsonPropertyName("openid.assoc_handle")]
        public string? AssocHandle { get; set; }
        [JsonPropertyName("openid.claimed_id")]
        public string? ClaimedId { get; set; }
        [JsonPropertyName("openid.identity")]
        public string? Identity { get; set; }
        [JsonPropertyName("openid.mode")]
        public string? Mode { get; set; }
        [JsonPropertyName("openid.ns")]
        public string? NS { get; set; }
        [JsonPropertyName("openid.op_endpoint")]
        public string? OpEndpoint { get; set; }
        [JsonPropertyName("openid.response_nonce")]
        public string? ResponseNonce { get; set; }
        [JsonPropertyName("openid.return_to")]
        public string? ReturnTo { get; set; }
        [JsonPropertyName("openid.sig")]
        public string? Sig { get; set; }
        [JsonPropertyName("openid.signed")]
        public string? Signed { get; set; }

        public static OpenIdParameters Build(IQueryCollection query)
            => new OpenIdParameters()
            {
                AssocHandle = query.TryGetValue("openid.assoc_handle", out StringValues assocHandle) ? assocHandle.FirstOrDefault() : null,
                ClaimedId = query.TryGetValue("openid.claimed_id", out StringValues claimedId) ? claimedId.FirstOrDefault() : null,
                Identity = query.TryGetValue("openid.identity", out StringValues identity) ? identity.FirstOrDefault() : null,
                Mode = query.TryGetValue("openid.mode", out StringValues mode) ? mode.FirstOrDefault() : null,
                NS = query.TryGetValue("openid.ns", out StringValues ns) ? ns.FirstOrDefault() : null,
                OpEndpoint = query.TryGetValue("openid.op_endpoint", out StringValues opEndpoint) ? opEndpoint.FirstOrDefault() : null,
                ResponseNonce = query.TryGetValue("openid.response_nonce", out StringValues responseNonce) ? responseNonce.FirstOrDefault() : null,
                ReturnTo = query.TryGetValue("openid.return_to", out StringValues returnTo) ? returnTo.FirstOrDefault() : null,
                Sig = query.TryGetValue("openid.sig", out StringValues sig) ? sig.FirstOrDefault() : null,
                Signed = query.TryGetValue("openid.signed", out StringValues signed) ? signed.FirstOrDefault() : null,
            };

        public Dictionary<string, string> ToDictionary()
            => new Dictionary<string, string>()
            {
                ["openid.assoc_handle"] = AssocHandle ?? string.Empty,
                ["openid.claimed_id"] = ClaimedId ?? string.Empty,
                ["openid.identity"] = Identity ?? string.Empty,
                ["openid.mode"] = Mode ?? string.Empty,
                ["openid.ns"] = NS ?? string.Empty,
                ["openid.op_endpoint"] = OpEndpoint ?? string.Empty,
                ["openid.response_nonce"] = ResponseNonce ?? string.Empty,
                ["openid.return_to"] = ReturnTo ?? string.Empty,
                ["openid.sig"] = Sig ?? string.Empty,
                ["openid.signed"] = Signed ?? string.Empty,
            };

        public FormUrlEncodedContent ToFormUrlEncodedContent()
            => new FormUrlEncodedContent(ToDictionary());

        public string GetSteamId()
            => ClaimedId?.Split('/').Last() ?? string.Empty;
    }
}
