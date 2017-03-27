using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Data;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Services
{
    public class MovieServiceAsync : IMovieServiceAsync
    {
        private IMovieRepository _movieRepository;
        private IMetaDataSearchRepository _metaDataRepository;
        public MovieServiceAsync(IMovieRepository movieRepository, IMetaDataSearchRepository metaDataRepository)
        {
            if (movieRepository == null) throw new ArgumentNullException(nameof(movieRepository));
            if (metaDataRepository == null) throw new ArgumentNullException(nameof(metaDataRepository));

            _movieRepository = movieRepository;
            _metaDataRepository = metaDataRepository;
        }
        public async Task<bool> AddMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.Add(movie).ContinueWith((i) => true);
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.Update(movie.Id, movie).ContinueWith((i) => true);
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.Delete(movie.Id).ContinueWith((i) => true);
        }

        public async Task<Movie> FetchMovieMetadata(SearchParameters paremeters)
        {
            if (paremeters == null) throw new ArgumentNullException(nameof(paremeters));
            if (!paremeters.IsValid()) throw new InvalidSearchParametersException(paremeters.ToString());

            return await _metaDataRepository.Search(paremeters);
        }

        public async Task<IList<Movie>> SearchAllMovies(SearchFilters filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            if (!filters.IsValid()) throw new InvalidSearchFilterException(filters.ToString());

            return await _movieRepository.Get(filters);
        }
    }
}