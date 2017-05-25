using Nancy.ErrorHandling;
using Nancy;
using System;
using Microsoft.AspNetCore.Hosting;

namespace wheresmymovies
{
    public class AppStatusCodeHandler : IStatusCodeHandler
    {
        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            context.Response.StatusCode = statusCode;
            context.Response.WithContentType("application/json");
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return !((IHostingEnvironment)context.Environment["Environment"]).IsDevelopment();
        }
    }
}
