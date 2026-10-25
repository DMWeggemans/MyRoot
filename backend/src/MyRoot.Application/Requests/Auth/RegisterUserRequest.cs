namespace MyRoot.Application.Requests.Auth
{
    public class RegisterUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
