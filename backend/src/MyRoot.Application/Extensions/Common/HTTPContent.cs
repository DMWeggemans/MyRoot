namespace MyRoot.Application.Extensions.Common
{
    public class HTTPContent
    {
        public Dictionary<string, string> Parameters { get; set; } = [];

        public static HTTPContent Create()
            => new HTTPContent();

        public HTTPContent WithParameter(string key, string value)
        {
            Parameters.Add(key, value);
            return this;
        }

        public FormUrlEncodedContent ToFormUrlEncodedContent()
            => new FormUrlEncodedContent(Parameters);
    }
}
