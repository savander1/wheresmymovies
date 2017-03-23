using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Services
{
    public class MovieService : IMovieService
    {
        public void AddMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> FetchMovieMetadata(SearchParameters paremeters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Movie>> SearchAllMovies(SearchFilters filters)
        {
            
            return Task.Factory.StartNew<IList<Movie>>(() =>
            {
                var retVal = new List<Movie>();
                //for (var i = 0; i < 100; i++)
                //{
                //    var movie = new Movie
                //    {
                //        Id = i.ToString()
                //    };
                //    retVal.Add(movie);
                //}
                return retVal;
            });
        }

        public void UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}