using Nancy;
using Nancy.TinyIoc;
using wheresmymovies.Data.Client;
using wheresmymovies.Services;

namespace wheresmymovies
{
    public class AppBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IHttpClient>(new HttpClientEx());
            container.Register<IInfoClient>(new OmovieClient(Config.IInfoUrl, container.Resolve<IHttpClient>()));
            container.Register<ISearchClient, AzureSearchClient>();
            //container.Register<IMovieService>(new MovieService());
        }
    }
}
