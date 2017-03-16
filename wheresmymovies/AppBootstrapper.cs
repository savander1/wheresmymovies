using Nancy;
using Nancy.TinyIoc;
using wheresmymovies.Data;

namespace wheresmymovies
{
    public class AppBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            //container.Register<IMovieRepository>(new MovieRepository());
            //container.Register
        }
    }
}
