using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using System.Threading.Tasks;

namespace wheresmymovies.Data
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> Search(MovieSearchParameters searchParams);
        Task<int> Add(Movie movie);
        Task<int> Update(string id, Movie movie);
        Task<int> Delete(string movie);
    }
}
