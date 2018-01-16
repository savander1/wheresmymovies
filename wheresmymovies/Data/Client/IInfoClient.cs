using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data.Client
{
    public interface IInfoClient
    {
        Task<IList<Movie>> SearchForMoviesAsync(SearchParameters parameters);
    }
}
