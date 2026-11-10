using MyRoot.Application.Interfaces;

namespace MyRoot.Application.HTML
{
    public class HtmlTemplate(IWebHostEnvironment _environment) : IHtmlTemplate
    {
        public async Task<string> GetAsync(string location, Dictionary<string, string>? variables = null)
        {
            string[] locations = location.Split('/');

            string path = Path.Combine([_environment.ContentRootPath, "HTML", ..locations]);
            if (!File.Exists(path))
                throw new InvalidOperationException($"The HTML template from location '{location}' not found!");

            string body = await File.ReadAllTextAsync(path);
            if (variables is null)
                return body;

            foreach (KeyValuePair<string, string> variable in variables)
                body = body.Replace($"{{{{ {variable.Key} }}}}", variable.Value);

            return body;
        }
    }
}
