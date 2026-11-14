using MyRoot.Domain.Interfaces.Clients.Games;
using MyRoot.Domain.Responses.Games.Xbox;
using Microsoft.Extensions.Configuration;
using MyRoot.Domain.Extensions;
using MyRoot.Domain.Models;
using System.Net.Http.Json;

namespace MyRoot.Infrastructure.Clients.Games
{
    public class XboxClient(HttpClient _httpClient, IConfiguration _configuration) : IXboxClient
    {
        public async Task<Xbox.GetDeviceCodeResponse?> GetDeviceCodeAsync()
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_connect.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HttpClientContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("scope", "Xboxlive.signin Xboxlive.offline_access")
                    .WithParameter("response_type", "device_code")
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<Xbox.GetDeviceCodeResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Xbox.AuthTokensResponse?> CheckDeviceCodeAsync(string deviceCode)
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_token.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HttpClientContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("grant_type", "urn:ietf:params:oauth:grant-type:device_code")
                    .WithParameter("device_code", deviceCode)
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<Xbox.AuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Xbox.AuthTokensResponse?> RefreshTokensAsync(string refreshToken)
        {
            string key = _configuration.GetValueOrThrow("APIs:XboxAPI:Key");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.live.com/oauth20_token.srf");
                request.Headers.ExpectContinue = false;
                request.Content = HttpClientContent.Create()
                    .WithParameter("client_id", key)
                    .WithParameter("grant_type", "refresh_token")
                    .WithParameter("refresh_token", refreshToken)
                    .ToFormUrlEncodedContent();

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return await response.Content.ReadFromJsonAsync<Xbox.AuthTokensResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
