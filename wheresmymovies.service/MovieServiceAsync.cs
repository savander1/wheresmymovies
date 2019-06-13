using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wheresmymovies.entities;
using wheresmymovies.service.Validator;

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
            _validator.Validate(movie, GetCreateValidationParameters());
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

        private string[] GetCreateValidationParameters()
        {
            var parameters = new List<string>
            {
                nameof(Movie.Actors),
                nameof(Movie.Description),
                nameof(Movie.Directors),
                nameof(Movie.FormatLocations),
                nameof(Movie.FullImgUrl),
                nameof(Movie.Genres),
                nameof(Movie.Runtime),
                nameof(Movie.ThumbImgUrl),
                nameof(Movie.Title),
                nameof(Movie.Writers),
                nameof(Movie.Years)
            };

            return parameters.ToArray();
        }
    }
}
