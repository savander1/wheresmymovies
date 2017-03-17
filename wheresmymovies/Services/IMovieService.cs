using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Services
{
    public interface IMovieService
    {
        Task<Movie> FetchMovieMetadata(SearchParameters paremeters);
        void AddMovie(Movie movie);
        void DeleteMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Task<IList<Movie>> SearchAllMovies(SearchFilters filters);

    }
}