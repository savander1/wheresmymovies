using com.shanevandermeer.wheresmymovies.Models;
using System;
using System.Collections.Generic;

namespace com.shanevandermeer.wheresmymovies.Repository
{
    public class MovieRepository : IRepository<Movie>
    {
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Movie Get(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Movie t)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, Movie t)
        {
            throw new NotImplementedException();
        }
    }
}
