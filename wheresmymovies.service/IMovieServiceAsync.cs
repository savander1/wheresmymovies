using System.Threading;
using System.Threading.Tasks;
using wheresmymovies.entities;

namespace wheresmymovies.service
{
    public interface IMovieServiceAsync
    {
        Task<Movie> Create(Movie movie, CancellationToken token);
        Task<Movie> Get(int id, CancellationToken token);
        Task<PagedResult<Movie>> GetAll(Page page, CancellationToken token);
        Task<PagedResult<Movie>> Find (MovieQuery query, CancellationToken token);
        Task<Movie> Update(int id, Movie movie, CancellationToken token);
        Task Delete(int id, CancellationToken token);
    }
}
