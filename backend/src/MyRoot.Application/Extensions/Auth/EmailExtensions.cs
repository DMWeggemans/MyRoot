using System.Net.Mail;

namespace MyRoot.Application.Extensions.Auth
{
    public static class EmailExtensions
    {
        public static bool IsValid(string email)
        {
            if (!MailAddress.TryCreate(email, out MailAddress? address))
                return false;

            return string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase);
        }
    }
}
