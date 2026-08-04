namespace MyRoot.Application.DTOs.Auth
{
    public class AuthTokens
    {
        public AuthToken JWT { get; set; }
        public AuthToken RefreshToken { get; set; }

        public AuthTokens(AuthToken jwt, AuthToken refreshToken)
        {
            JWT = jwt;
            RefreshToken = refreshToken;
        }
    }
}
