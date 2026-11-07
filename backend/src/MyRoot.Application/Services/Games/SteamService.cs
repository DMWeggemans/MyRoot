using MyRoot.Application.Interfaces.Clients.Games;
using MyRoot.Application.Interfaces.Services.Games;
using MyRoot.Application.Responses.Games.Steam;
using MyRoot.Application.Requests.Games.Steam;
using MyRoot.Application.DTOs.Games.Steam;
using MyRoot.Domain.Configurations;
using MyRoot.Application.DTOs.Auth;
using MyRoot.Application.DTOs;
using System.Text.Json;

namespace MyRoot.Application.Services.Games
{
    public class SteamService(HttpClient _httpClient, ISteamClient _steamClient) : ISteamService
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<Result<string>> GetSteamIdAsync(OpenIdParameters parameters)
        {
            try
            {
                if (parameters.Mode is null)
                    return Result<string>.Failed(value: "<h2>Authentication Failed: Missing OpenID Parameters</h2>");

                parameters.Mode = "check_authentication";
                using var content = parameters.ToFormUrlEncodedContent();
                HttpResponseMessage response = await _httpClient.PostAsync("https://steamcommunity.com/openid/login", content);

                string? body = await response.Content.ReadAsStringAsync();
                if (body is null || !body.Contains("is_valid:true"))
                    throw new Exception();
            }
            catch (Exception)
            {
                return Result<string>.Failed("<h2>Steam OpenID signature verification failed.</h2>");
            }

            SteamPlayerSummary? player = await _steamClient.GetPlayerAsync(parameters.GetSteamId());
            List<SteamGameSummary> games = await _steamClient.GetPlayerGamesAsync(parameters.GetSteamId());

            string payload = JsonSerializer.Serialize(Result<LinkSteamResponse>.Success(new LinkSteamResponse(parameters.GetSteamId(), player, games)), JsonOptions);
            return Result<string>.Success($@"
                    <!DOCTYPE html>
                    <html style=""overflow: hidden; background-color: #1b2838;"">
                        <head>
                            <title>Syncing Steam to myroot.dev</title>
                        </head>
                        <body style=""display: grid; place-items: center; min-height: 100vh; width: 100%;"">
                            <p style=""font-family: Motiva Sans, Motiva Sans fallback, Arial, Helvetica, sans-serif; color: #67c1f5; font-size: 1.2rem;"">Authenticating account, please wait<span class=""loader""></span></p>
                            <script defer>
                                setTimeout(() => {{
                                    window.opener.postMessage({payload}, ""{ApplicationConfiguration.Urls.Application}"");
                                    window.close();
                                }}, 1500);

                                let element = null;
                                setInterval(() => {{
                                    if (element === null)
                                        element = document.querySelector("".loader"");

                                    if (element.textContent.length >= 3) {{
                                        element.textContent = """";
                                    }} else {{
                                        element.textContent += ""."";
                                    }}
                                }}, 500);
                            </script>
                        </body>
                    </html>");
        }

        public async Task<Result> LinkSteamAsync(LinkSteamRequest request, Guid userId)
        {
            //SteamPlayerSummary? player = await _steamClient.GetPlayerAsync(request.SteamId);
            //List<SteamGameSummary> games = await _steamClient.GetPlayerGamesAsync(request.SteamId);

            //if (request.SteamId != player?.SteamId || request.UserName != player?.UserName)
            //    return Result.Failed("INVALID_REQUEST");

            //LinkSteamResponse response = new LinkSteamResponse(request.SteamId, player, games);
            //if (!response.IsPublic)
            //    return Result.Failed("PRIVATE_USER");

            //Guid accountId = _context.Accounts
            //    .Where(x => x.UserId == userId)
            //    .Select(x => x.Id)
            //    .SingleOrDefault();

            //if (accountId == Guid.Empty)
            //{
            //    Account account = new Account()
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = AccountType.Game,
            //        UserId = userId,
            //        CreatedOn = DateTimeOffset.UtcNow
            //    };

            //    _context.Accounts.Add(account);
            //    accountId = account.Id;
            //}

            //GameProvider provider = new GameProvider()
            //{
            //    Provider = "Steam",
            //    UserName = response.UserName,
            //    UserId = response.SteamId,
            //    AccountId = accountId,
            //    LinkedOn = DateTimeOffset.UtcNow
            //};

            //_context.GameProviders.Add(provider);
            //await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
