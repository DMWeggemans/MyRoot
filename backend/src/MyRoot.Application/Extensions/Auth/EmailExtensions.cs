using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MyRoot.Application.Extensions.Auth
{
    public static class EmailExtensions
    {
        public static bool IsValid(string email)
        {
            if (!MailAddress.TryCreate(email, out MailAddress? address))
                return false;

            bool matchesEmail = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").IsMatch(email);
            return address.Address == email && matchesEmail;
        }
    }
}
