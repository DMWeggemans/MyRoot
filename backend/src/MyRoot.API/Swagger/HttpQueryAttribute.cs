using Microsoft.AspNetCore.Mvc.Routing;

namespace MyRoot.API.Swagger
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpQueryAttribute : HttpMethodAttribute
    {
        private static readonly IEnumerable<string> Methods = ["QUERY"];

        public HttpQueryAttribute(string route = "")
            : base(Methods, route) { }
    }
}
