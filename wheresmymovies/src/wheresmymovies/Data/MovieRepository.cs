using System;
using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class MovieRepository : IMovieRepository
    {
        public void Add(Movie moive)
        {
            throw new NotImplementedException();
        }

        public void Delete(string movie)
        {
            throw new NotImplementedException();
        }

        public ICollection<Movie> Search(MovieSearchParameters searchParams)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
