using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfigurationRoot _config;

        public AppBootstrapper(IHostingEnvironment env, IConfigurationRoot config)
        {
            _env = env;
            _config = config;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IHttpClient>(new HttpClientEx());
            container.Register<IInfoClient>(new OmovieClient(Config.IInfoUrl, container.Resolve<IHttpClient>()));
            container.Register<ISearchClient>(new AzureSearchClient(GetAzureSearchConfiguration()));
            container.Register<IMovieRepositoryAsync>(new MovieRepositoryAsync(container.Resolve<ISearchClient>(), container.Resolve<IInfoClient>()));
            container.Register<IMovieServiceAsync>(new MovieServiceAsync(container.Resolve<IMovieRepositoryAsync>()));
        }

        private AzureSearchConfiguration GetAzureSearchConfiguration()
        {
            return new AzureSearchConfiguration(_config.GetValue<string>("AzureSearchApiKey"), _config.GetValue<string>("AzureApiEndpoint"));
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.AddValue("Environment", _env);
            base.Configure(environment);
        }
    }
}