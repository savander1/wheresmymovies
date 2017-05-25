using Microsoft.AspNetCore.Hosting;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using wheresmymovies.Data;
using wheresmymovies.Data.Client;
using wheresmymovies.Services;

namespace wheresmymovies
{
    public class AppBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IHostingEnvironment _env;

        public AppBootstrapper(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IHttpClient>(new HttpClientEx());
            container.Register<IInfoClient>(new OmovieClient(Config.IInfoUrl, container.Resolve<IHttpClient>()));
            container.Register<ISearchClient>(new AzureSearchClient(Config.GetAzureSearchConfiguration()));
            container.Register<IMovieRepositoryAsync>(new MovieRepositoryAsync(container.Resolve<ISearchClient>(), container.Resolve<IInfoClient>()));
            container.Register<IMovieServiceAsync>(new MovieServiceAsync(container.Resolve<IMovieRepositoryAsync>()));
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.AddValue("Environment", _env);
            base.Configure(environment);
        }
    }
}