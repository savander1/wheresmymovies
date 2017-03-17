using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using wheresmymovies.Models;
using wheresmymovies.Services;

namespace wheresmymovies.Controllers
{
    public class MovieModule : NancyModule
    {
        public MovieModule(IMovieService movieService) : base("/api/movie")
        {
            Get("/", async (x, ctx) =>
            {
                var searchFilters = this.BindTo((SearchFilters)x);
                var movies = await movieService.SearchAllMovies(searchFilters);
                if (movies == null || !movies.Any())
                {
                    return HttpStatusCode.NotFound;
                }

                return movies;
            });

            //Post("/", movie =>
            //{
            //    movieRepository.Add(movie);
            //    return HttpStatusCode.OK;
            //});

        }
    }
}
