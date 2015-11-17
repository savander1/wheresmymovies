using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using System.Threading.Tasks;

namespace wheresmymovies.Data
{
    public interface IMovieRepository
    {
        Task<int> Add(Movie movie);
        Task<int> Update(string id, Movie movie);
        Task<int> Delete(string id);
        Task<Movie> Get(string id);
        ICollection<Movie> Get(MovieSearchParameters searchParams);
    }
}
