namespace MyRoot.Domain.Configurations
{
    public static class ApplicationConfiguration
    {
        public static class Urls
        {
            public static string Application = string.Empty;
            public static string API = string.Empty;
        }

        public static class Auth
        {
            public static class Tokens
            {
                public static readonly string AccessToken = "X-Access-Token";
                public static readonly string RefreshToken = "X-Refresh-Token";
            }
        }
    }
}
