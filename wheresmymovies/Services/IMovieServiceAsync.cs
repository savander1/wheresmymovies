using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Services
{
    public interface IMovieServiceAsync
    {
        Task<IList<Movie>> FetchMovieMetadata(SearchParameters paremeters);
        Task<bool> AddMovie(Movie movie);
        Task<bool> DeleteMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<IList<Movie>> SearchAllMovies(SearchFilters filters);

    }
}