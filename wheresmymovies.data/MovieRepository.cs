using System;
using System.Collections.Generic;
using System.Linq;
using wheresmymovies.entities;
using wheresmymovies.entities.Filter;

namespace wheresmymovies.data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDbContextFactory<MovieContext> _dbContextFactory;
        protected MovieRepository(IDbContextFactory<MovieContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public void Delete(int id)
        {
            using(var context = _dbContextFactory.GetContext())
            {
                var movie = context.Find<Movie>(id);
                if (movie != null)
                {
                    context.Remove(movie);
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<Movie> Find(IFilter<Movie> filter)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                var foo = context.Set<Movie>().Where(movie => movie.Id == 1).AsEnumerable();
            }
            throw new NotImplementedException();
        }

        public Movie Get(int id)
        {
            using(var context = _dbContextFactory.GetContext())
            {
                return context.Find<Movie>(id);
            }
        }

        public Movie Save(Movie t)
        {
            using(var context = _dbContextFactory.GetContext())
            {
                var saved = context.Add(t);
                context.SaveChanges();
                return saved.Entity;
            }
        }

        public Movie Update(int id, Movie t)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                var movie = context.Find<Movie>(id);
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

                context.Update(movie);
                context.SaveChanges();
                return movie;
            }
        }
    }
}
