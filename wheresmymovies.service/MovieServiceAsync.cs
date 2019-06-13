using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wheresmymovies.entities;
using wheresmymovies.service.Validation;

namespace wheresmymovies.service
{
    public class MovieServiceAsync : IMovieServiceAsync
    {
        private readonly MovieValidatorAsync _validator;
        public MovieServiceAsync(MovieValidatorAsync validator)
        {
            _validator = validator;
        }

        public Task<Movie> Create(Movie movie, CancellationToken token)
        {
            _validator.Validate(movie);
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Movie>> Find(MovieQuery query, Page page, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Get(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Update(int id, Movie movie, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
