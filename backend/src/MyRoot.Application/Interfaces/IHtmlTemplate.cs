namespace MyRoot.Application.Interfaces
{
    public interface IHtmlTemplate
    {
        Task<string> GetAsync(string location, Dictionary<string, string>? variables = null);
    }
}
