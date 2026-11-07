using System.Text.Encodings.Web;

namespace MyRoot.Application.Extensions.Common
{
    public class URL
    {
        public string Url { get; set; } = string.Empty;

        public static URL Create(string url)
            => new URL()
            {
                Url = url
            };

        public static string? GetParameter(string url, string key)
        {
            if (!key.Contains('='))
                key += '=';

            int startIndex = url.IndexOf(key) + key.Length;
            if (startIndex < 0)
                return null;

            int endIndex = url.IndexOf('&', startIndex);
            if (endIndex < 0)
                endIndex = key.Length - 1;

            return url[startIndex..endIndex];
        }

        public URL WithParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return this;

            Url += !Url.Contains('?') ? '?' : '&';

            UrlEncoder encoder = UrlEncoder.Create();
            Url += $"{encoder.Encode(key)}={encoder.Encode(value)}";

            return this;
        }

        public override string ToString()
            => Url;
    }
}
