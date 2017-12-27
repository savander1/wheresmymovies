using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using System.Threading.Tasks;

namespace wheresmymovies.Data
{
    public interface IMovieRepositoryAsync
    {
        Task<int> AddAsync(Movie movie);
        Task<int> UpdateAsync(string id, Movie movie);
        Task<int> DeleteAsync(string id);
        Task<Movie> GetAsync(string id);
        Task<List<Movie>> GetAsync(SearchFilters searchFilters);
        Task<IList<Movie>> SearchAsync(SearchParameters searchParams);
    }
}
