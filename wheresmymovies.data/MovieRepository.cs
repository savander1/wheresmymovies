using System;
using System.Collections.Generic;
using wheresmymovies.entities;
using wheresmymovies.entities.Filter;

namespace wheresmymovies.data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        public MovieRepository(MovieContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Delete(int id)
        {
            var movie = _context.Find<Movie>(id);
            if (movie != null)
            {
                _context.Remove(movie);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Movie> Find(IFilter<Movie> filter)
        {
            throw new NotImplementedException();
        }

        public Movie Get(int id)
        {
            return _context.Find<Movie>(id);
        }

        public Movie Save(Movie t)
        {
            var saved = _context.Add(t);
            _context.SaveChanges();
            return saved.Entity;
        }

        public Movie Update(int id, Movie t)
        {
            var movie = _context.Find<Movie>(id);
            movie.Actors = t.Actors;
            movie.Description = t.Description;
            movie.Directors = t.Directors;
            movie.FormatLocations = t.FormatLocations;
            movie.FullImgUrl = t.FullImgUrl;
            movie.Genres = t.Genres;
            movie.Runtime = t.Runtime;
            movie.ThumbImgUrl = t.ThumbImgUrl;
            movie.Title = t.Title;
            movie.Writers = t.Writers;
            movie.Years = t.Years;

            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
