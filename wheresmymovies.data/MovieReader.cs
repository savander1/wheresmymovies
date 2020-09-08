using System;
using System.Collections.Generic;
using System.Data;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class MovieReader : IEntityReader<Movie>
    {
        public Movie Read(IDataReader reader)
        {
            var movie = new Movie();
            while (reader.Read())
            {
                movie.Id = GetInt(nameof(Movie.Id), reader);
                movie.Title = GetString(nameof(Movie.Title), reader);
                movie.ThumbImgUrl = GetString(nameof(Movie.ThumbImgUrl), reader);
                movie.Description = GetString(nameof(Movie.Description), reader);
                movie.FullImgUrl = GetString(nameof(Movie.FullImgUrl), reader);
                movie.Runtime = GetLong(nameof(Movie.Runtime), reader);
            }

            reader.NextResult();
            movie.Years = GetMovieYears(reader);

            reader.NextResult();
            movie.Genres = GetMovieGenres(reader);

            reader.NextResult();
            movie.Directors = GetMovieDirectors(reader);

            reader.NextResult();
            movie.Writers = GetMovieWriters(reader);

            reader.NextResult();
            movie.Actors = GetMovieActors(reader);

            reader.NextResult();
            movie.FormatLocations = GetMovieFormatLocations(reader); ;

            return movie;
        }

        private ICollection<MovieYear> GetMovieYears(IDataReader reader)
        {
            var years = new List<MovieYear>();
            while (reader.Read())
            {
                years.Add(new MovieYear
                {
                    Id = GetInt(nameof(MovieYear.Id), reader),
                    Year = GetInt(nameof(MovieYear.Year), reader)
                });
            }
            return years;
        }

        private ICollection<MovieGenre> GetMovieGenres(IDataReader reader)
        {
            var genres = new List<MovieGenre>();
            while (reader.Read())
            {
                genres.Add(new MovieGenre
                {
                    Id = GetInt(nameof(MovieGenre.Id), reader),
                    Genre = GetString(nameof(MovieGenre.Genre), reader)
                });
            }
            return genres;
        }

        private ICollection<MovieDirector> GetMovieDirectors(IDataReader reader)
        {
            var directors = new List<MovieDirector>();
            while (reader.Read())
            {
                directors.Add(new MovieDirector
                {
                    Id = GetInt(nameof(MovieDirector.Id), reader),
                    Director = GetString(nameof(MovieDirector.Director), reader)
                });
            }
            return directors;
        }

        private ICollection<MovieWriter> GetMovieWriters(IDataReader reader)
        {
            var writers = new List<MovieWriter>();
            while (reader.Read())
            {
                writers.Add(new MovieWriter
                {
                    Id = GetInt(nameof(MovieWriter.Id), reader),
                    Writer = GetString(nameof(MovieWriter.Writer), reader)
                });
            }
            return writers;
        }

        private ICollection<MovieActor> GetMovieActors(IDataReader reader)
        {
            var actors = new List<MovieActor>();
            while (reader.Read())
            {
                actors.Add(new MovieActor
                {
                    Id = GetInt(nameof(MovieActor.Id), reader),
                    Actor = GetString(nameof(MovieActor.Actor), reader)
                }); ;
            }
            return actors;
        }

        private ICollection<MovieFormatLocation> GetMovieFormatLocations(IDataReader reader)
        {
            var locations = new List<MovieFormatLocation>();
            while (reader.Read())
            {
                locations.Add(new MovieFormatLocation
                {
                    Id = GetInt(nameof(MovieFormatLocation.Id), reader),
                    Format = GetString(nameof(MovieFormatLocation.Format), reader),
                    Location = GetString(nameof(MovieFormatLocation), reader)
                });
            }

            return locations;
        }

        public IEnumerable<Movie> ReadList(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        private string GetString(string name, IDataReader reader)
        {
            var pos = reader.GetOrdinal(name);
            return reader.IsDBNull(pos) ? null : reader.GetString(pos); 
        }

        private int GetInt(string name, IDataReader reader)
        {
            var pos = reader.GetOrdinal(name);
            return reader.IsDBNull(pos) ? 0 : reader.GetInt32(pos);
        }

        private long GetLong(string name, IDataReader reader)
        {
            var pos = reader.GetOrdinal(name);
            return reader.IsDBNull(pos) ? 0 : reader.GetInt64(pos);
        }
    }
}
