// DELETE THIS CLASS ONCE WE HAVE A PROPER WAY TO GET METADATA
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data.Client
{
    public class DefaultInfoClient : IInfoClient
    {
        public Task<Movie> GetMovieAsync(SearchParameters parameters)
        {
            return new Task<Movie>(() =>
            {
                return new Movie();
            });
        }
    }
}
