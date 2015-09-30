using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public interface IMovieRepository
    {
        ICollection<Movie> Search(MovieSearchParameters searchParams);
        void Add(Movie moive);
        void Update(Movie movie);
        void Delete(string movie);
    }
}
