using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Services;

namespace wheresmymovies.Controllers
{
    public class MovieModule : NancyModule
    {
        public MovieModule(IMovieServiceAsync movieService) : base("/api/movie")
        {
            Get("/", async (x, ctx) =>
            {
                var searchFilters = new SearchFilters();
                this.BindTo(searchFilters);
                var movies = await movieService.SearchAllMovies(searchFilters);
                if (movies == null || !movies.Any())
                {
                    return HttpStatusCode.NotFound;
                }
                
                return movies;
            });

            Get("/search", async (x, ctx) =>
            {
                var searchParams = new SearchParameters();
                this.BindTo(searchParams);
                var movie = await movieService.FetchMovieMetadata(searchParams.Decode());
                if (movie == null)
                    return HttpStatusCode.NotFound;

                return movie;
            });

            Post("/", async (x, ctx) =>
            {
                var movie = new Movie();
                this.BindTo(movie);
                var success =await movieService.AddMovie(movie);
                return success ? HttpStatusCode.OK : HttpStatusCode.NotModified;
            });

            Put("/", async (x, ctx) =>
            {
                var movie = new Movie();
                this.BindTo(movie);
                var success = await movieService.UpdateMovie(movie);
                return success ? HttpStatusCode.OK : HttpStatusCode.NotModified;
            });

            Delete("/", async (x, ctx) =>
            {
                var movie = new Movie();
                this.BindTo(movie);
                var success = await movieService.DeleteMovie(movie);
                return success ? HttpStatusCode.OK : HttpStatusCode.NotModified;
            });

        }
    }
}
