using System.Collections.Generic;
using wheresmymovies.api.Models;

namespace wheresmymovies.api.Service
{
    public interface IMovieService
    {
        Movie Get(int id);
        Movie Save(Movie value);
        Movie Update(int id, Movie value);
        void Delete(int id);
        IEnumerable<Movie> Find(Movie value);
    }
}
