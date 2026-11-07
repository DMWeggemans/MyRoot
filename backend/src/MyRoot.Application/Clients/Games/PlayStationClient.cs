using MyRoot.Application.DTOs.Games.PlayStation;
using MyRoot.Application.Extensions.Games.PlayStation;
using MyRoot.Application.Responses.Games.PlayStation;
using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Extensions.Common;
using MyRoot.Application.DTOs.Games;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace MyRoot.Application.Clients.Games
{
    public class PlayStationClient(HttpClient _httpClient) : IPlayStationClient
    {
        public async Task<string?> GetCodeByNpssoAsync(string npsso)
        {
            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL.Create("https://ca.account.sony.com/api/authz/v3/oauth/authorize")
                    .WithParameter("access_type", "offline")
                    .WithParameter("client_id", "09515159-7237-4370-9b40-3806e67c0891")
                    .WithParameter("response_type", "code")
                    .WithParameter("scope", "psn:mobile.v2.core psn:clientapp")
                    .WithParameter("redirect_uri", "com.scee.psxandroid.scecompcall://redirect")
                    .ToString());
                request.Headers.Add("Cookie", $"npsso={npsso}");

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (response.StatusCode != HttpStatusCode.Found)
                    return null;

                string location = response.Headers.Location?.ToString() ?? string.Empty;
                return URL.GetParameter(location, "code");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PlayStationAuthTokensResponse?> GetTokensByCodeAsync(string code)
        {
            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://ca.account.sony.com/api/authz/v3/oauth/token");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                request.Content = HTTPContent.Create()
                    .WithParameter("code", code)
                    .WithParameter("redirect_uri", "com.scee.psxandroid.scecompcall://redirect")
                    .WithParameter("grant_type", "authorization_code")
                    .WithParameter("token_format", "jwt")
                    .ToFormUrlEncodedContent();

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<PlayStationAuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PlayStationAuthTokensResponse?> RefreshTokensAsync(string refreshToken)
        {
            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://ca.account.sony.com/api/authz/v3/oauth/token");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                request.Content = HTTPContent.Create()
                    .WithParameter("refresh_token", refreshToken)
                    .WithParameter("grant_type", "refresh_token")
                    .WithParameter("scope", "psn:mobile.v2.core psn:clientapp")
                    .WithParameter("token_format", "jwt")
                    .ToFormUrlEncodedContent();

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<PlayStationAuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PlayStationGetUserIdByUserNameResponse?> GetAccountIdByUserNameAsync(string accessToken, string userName)
        {
            UrlEncoder encoder = UrlEncoder.Create();

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL.Create($"https://us-prof.np.community.playstation.net/userProfile/v1/users/{encoder.Encode(userName)}/profile2")
                    .WithParameter("fields", "accountId,onlineId,currentOnlineId")
                    .ToString());
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<PlayStationGetUserIdByUserNameResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PlayStationGetProfileByAccountIdResponse?> GetProfileByAccountIdAsync(string accessToken, string accountId)
        {
            UrlEncoder encoder = UrlEncoder.Create();

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://m.np.playstation.com/api/userProfile/v1/internal/users/{encoder.Encode(accountId)}/profiles");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using HttpResponseMessage response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<PlayStationGetProfileByAccountIdResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
