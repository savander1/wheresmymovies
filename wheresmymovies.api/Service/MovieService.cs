using System;
using System.Collections.Generic;
using wheresmymovies.api.Models;
using wheresmymovies.data;

namespace wheresmymovies.api.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMovieMapper mapper)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            throw new NotImplementedException();
        }
    }
}
