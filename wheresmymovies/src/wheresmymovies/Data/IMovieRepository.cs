using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using System.Threading.Tasks;

namespace wheresmymovies.Data
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> Search(MovieSearchParameters searchParams);
        Task<bool> Add(Movie movie);
        Task<bool> Update(string id, Movie movie);
        Task<bool> Delete(string movie);
    }
}
