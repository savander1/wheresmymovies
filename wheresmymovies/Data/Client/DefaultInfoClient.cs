using System;
using System.Collections.Generic;
// DELETE THIS CLASS ONCE WE HAVE A PROPER WAY TO GET METADATA
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data.Client
{
    public class DefaultInfoClient : IInfoClient
    {
        public Task<IList<Movie>> SearchForMoviesAsync(SearchParameters parameters)
        {
            var t = new Task<IList<Movie>>(() =>
            {
                return new List<Movie>();
            });
            t.Start();
            return t;
        }
    }
}
