namespace MyRoot.Application.Requests.Auth
{
    public class LoginUserRequest
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
