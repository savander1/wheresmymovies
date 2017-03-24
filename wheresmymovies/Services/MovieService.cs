using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Data;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _moveRepository;
        private IMetaDataSearchRepository _metaDataRepository;
        public MovieService(IMovieRepository movieRepository, IMetaDataSearchRepository metaDataRepository)
        {
            if (movieRepository == null) throw new ArgumentNullException(nameof(movieRepository));
            if (metaDataRepository == null) throw new ArgumentNullException(nameof(metaDataRepository));

            _metaDataRepository = metaDataRepository;
            _metaDataRepository = metaDataRepository;
        }
        public void AddMovie(Movie movie)
        {
             _moveRepository.Add(movie).Start();
        }

        public void DeleteMovie(Movie movie)
        {
            _moveRepository.Delete(movie.Id).Start();
        }

        public Task<Movie> FetchMovieMetadata(SearchParameters paremeters)
        {
            return _metaDataRepository.Search(paremeters);
        }

        public Task<IList<Movie>> SearchAllMovies(SearchFilters filters)
        {
            
            return Task.Factory.StartNew<IList<Movie>>(() =>
            {
                var retVal = new List<Movie>();
                for (var i = 0; i < 100; i++)
                {
                   var movie = new Movie
                   {
                       Id = i.ToString()
                   };
                   retVal.Add(movie);
                }
                return retVal;
            });
        }

        public void UpdateMovie(Movie movie)
        {
            _moveRepository.Update(movie.Id, movie).Start();
        }
    }
}