using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data.Client
{
    public interface IInfoClient
    {
        Task<Movie> GetMovieAsync(SearchParameters parameters);
    }
}
