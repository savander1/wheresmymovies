using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public interface IMetaDataSearchRepositoryAsync
    {
        Task<Movie> SearchAsync(SearchParameters searchParams);
    }
}
