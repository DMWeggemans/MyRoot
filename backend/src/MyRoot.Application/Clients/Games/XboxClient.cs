using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Extensions.Games.Xbox;
using MyRoot.Application.Responses.Games.Xbox;
using MyRoot.Application.Extensions.Common;
using Microsoft.Extensions.Configuration;
using MyRoot.Application.DTOs.Games.Xbox;
using MyRoot.Application.Extensions;
using System.Net.Http.Json;

namespace MyRoot.Application.Clients.Games
{
    public class XboxClient(HttpClient _httpClient, IConfiguration _configuration) : IXboxClient
    {
        public async Task<XboxGetDeviceCodeResponse?> GetDeviceCodeAsync()
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_connect.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HTTPContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("scope", "Xboxlive.signin Xboxlive.offline_access")
                    .WithParameter("response_type", "device_code")
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<XboxGetDeviceCodeResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<XboxAuthTokensResponse?> CheckDeviceCodeAsync(string deviceCode)
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_token.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HTTPContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("grant_type", "urn:ietf:params:oauth:grant-type:device_code")
                    .WithParameter("device_code", deviceCode)
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<XboxAuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<XboxAuthTokensResponse?> RefreshTokensAsync(string refreshToken)
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_token.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HTTPContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("grant_type", "refresh_token")
                    .WithParameter("refresh_token", refreshToken)
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<XboxAuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
