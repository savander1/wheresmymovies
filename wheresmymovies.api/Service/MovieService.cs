using System;
using System.Collections.Generic;
using System.Linq;
using wheresmymovies.api.Models;
using wheresmymovies.api.Service.Mapping;
using wheresmymovies.data;

namespace wheresmymovies.api.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieMapper _mapper;
        private readonly IMovieQueryMapper _queryMapper;

        public MovieService(IMovieRepository movieRepository, IMovieMapper mapper, IMovieQueryMapper queryMapper)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryMapper = queryMapper ?? throw new ArgumentNullException(nameof(queryMapper));
        }

        public void Delete(int id)
        {
            _movieRepository.Delete(id);
        }

        public Movie Get(int id)
        {
            return _mapper.ToModel(_movieRepository.Get(id)) ?? throw new MovieNotFoundException();
        }

        public Movie Save(Movie value)
        {
            return _mapper.ToModel(_movieRepository.Save(_mapper.ToEntity(value)));
        }

        public Movie Update(int id, Movie value)
        {
            return _mapper.ToModel(_movieRepository.Update(id, _mapper.ToEntity(value)));
        }

        public IEnumerable<Movie> Find(Movie value)
        {
            var query = new MovieQuery(value);
            var filter = _queryMapper.ToEntity(query);

            return _movieRepository.Find(filter).Select(_mapper.ToModel);
        }
    }
}
