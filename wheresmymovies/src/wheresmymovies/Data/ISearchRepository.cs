using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public interface ISearchRepository
    {
        Task<Movie> Search(MovieSearchParameters searchParams);
    }
}
