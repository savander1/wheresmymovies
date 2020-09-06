using System;
using System.Collections.Generic;
using System.Data;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class MovieReader : IEntityReader<Movie>
    {
        public Movie Read(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> ReadList(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
