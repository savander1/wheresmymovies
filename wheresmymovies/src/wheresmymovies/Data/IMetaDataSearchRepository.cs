using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public interface IMetaDataSearchRepository
    {
        Task<Movie> Search(MovieSearchParameters searchParams);
    }
}
