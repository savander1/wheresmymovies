using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public interface IMovieRepository
    {
        ICollection<Movie> Search(MovieSearchParameters searchParams);
        bool Add(Movie movie);
        bool Update(string id, Movie movie);
        bool Delete(string movie);
    }
}
