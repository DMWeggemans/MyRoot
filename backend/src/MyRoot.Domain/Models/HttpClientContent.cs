namespace MyRoot.Domain.Models
{
    public class HttpClientContent
    {
        public Dictionary<string, string> Parameters { get; set; } = [];

        public static HttpClientContent Create()
            => new HttpClientContent();

        public HttpClientContent WithParameter(string key, string value)
        {
            Parameters.Add(key, value);
            return this;
        }

        public FormUrlEncodedContent ToFormUrlEncodedContent()
            => new FormUrlEncodedContent(Parameters);
    }
}
