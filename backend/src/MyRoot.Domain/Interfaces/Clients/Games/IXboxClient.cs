using MyRoot.Domain.Responses.Games.Xbox;

namespace MyRoot.Domain.Interfaces.Clients.Games
{
    public interface IXboxClient
    {
        Task<Xbox.GetDeviceCodeResponse?> GetDeviceCodeAsync();
        Task<Xbox.AuthTokensResponse?> CheckDeviceCodeAsync(string deviceCode);
        Task<Xbox.AuthTokensResponse?> RefreshTokensAsync(string refreshToken);
    }
}
