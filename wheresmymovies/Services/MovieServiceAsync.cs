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
        private IMovieRepositoryAsync _movieRepository;
        public MovieServiceAsync(IMovieRepositoryAsync movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }
        public async Task<bool> AddMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.AddAsync(movie).ContinueWith((i) => true);
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.UpdateAsync(movie.Id, movie).ContinueWith((i) => true);
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.DeleteAsync(movie.Id).ContinueWith((i) => true);
        }

        public async Task<Movie> FetchMovieMetadata(SearchParameters paremeters)
        {
            if (paremeters == null) throw new ArgumentNullException(nameof(paremeters));
            if (!paremeters.IsValid()) throw new InvalidSearchParametersException(paremeters.ToString());

            return await _movieRepository.SearchAsync(paremeters);
        }

        public async Task<IList<Movie>> SearchAllMovies(SearchFilters filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            if (!filters.IsValid()) throw new InvalidSearchFilterException(filters.ToString());

            return await _movieRepository.GetAsync(filters);
        }
    }
}