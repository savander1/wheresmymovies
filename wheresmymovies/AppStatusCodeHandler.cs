using Nancy.ErrorHandling;
using Nancy;
using Microsoft.AspNetCore.Hosting;

namespace wheresmymovies
{
    public class AppStatusCodeHandler : IStatusCodeHandler
    {
        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            context.Response.WithContentType("application/json");
            context.Response.WithStatusCode(statusCode);
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return !((IHostingEnvironment)context.Environment["Environment"]).IsDevelopment();
        }
    }
}
