using Nancy;
using Nancy.TinyIoc;
using wheresmymovies.Services;

namespace wheresmymovies
{
    public class AppBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IMovieService>(new MovieService());
        }
    }
}
