namespace MyRoot.Application.DTOs.Auth
{
    public class AuthToken
    {
        public string Value { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }

        public AuthToken(string value, DateTime expiresOn)
        {
            Value = value;
            ExpiresOn = expiresOn;
        }
    }
}
