using MyRoot.Application.Responses.Games.Xbox;

namespace MyRoot.Application.Interfaces.Clients.Games
{
    public interface IXboxClient
    {
        Task<XboxGetDeviceCodeResponse?> GetDeviceCodeAsync();
        Task<XboxAuthTokensResponse?> CheckDeviceCodeAsync(string deviceCode);
        Task<XboxAuthTokensResponse?> RefreshTokensAsync(string refreshToken);
    }
}
