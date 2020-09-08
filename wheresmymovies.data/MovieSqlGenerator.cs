using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class MovieSqlGenerator : ISqlGenerator<Movie, int>
    {
        private IDbConnection _connection;
        public IDbConnection connection { set => _connection = value; }

        public IDbCommand GenerateDelete(int id)
        {
            const string sql = "DeleteMovie.sql";
            var command = _connection.CreateCommand();
            command.CommandText = ReadCommandText(sql);
            var idParam = command.CreateParameter();
            idParam.ParameterName = "Id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            return command;
        }

        public IDbCommand GenerateFind(IQuery<Movie> query)
        {
            throw new NotImplementedException();
        }

        public IDbCommand GenerateGet(int id)
        {
            const string sql = "GetMovieById.sql";
            var command = _connection.CreateCommand();
            command.CommandText = ReadCommandText(sql);
            var idParam = command.CreateParameter();
            idParam.ParameterName = "Id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            return command;
        }

        public IDbCommand GenerateSave(Movie t)
        {
            const string sql = "InsertMovie.sql";
            var commandText = new StringBuilder(ReadCommandText(sql));
            var command = _connection.CreateCommand();
            
            SetParam(command, "Title", t.Title);
            SetParam(command, "ThumbImgUrl", t.ThumbImgUrl);
            SetParam(command, "Description", t.Description);
            SetParam(command, "Runtime", t.Runtime);
            SetParam(command, "FullImgUrl", t.FullImgUrl);


            var yearIn = 0;
            var yearIt = t.Years.GetEnumerator();
            while(yearIt.MoveNext())
            {
                var name = $"Year{yearIn}";
                commandText.AppendLine($"INSERT INTO MovieYear (Id, [Year]) VALUES (@Id, @{name})");
                SetParam(command, name, yearIt.Current.Year);
                yearIn++;

            }

            var genreIn = 0;
            var genreIt = t.Genres.GetEnumerator();
            while(genreIt.MoveNext())
            {
                var name = $"Genre{genreIn}";
                commandText.AppendLine($"INSERT INTO MovieGenre (Id, [Genre]) VALUES (@Id, @{name})");
                SetParam(command, name, genreIt.Current.Genre);
                genreIn++;
            }

            var directorIn = 0;
            var directorIt = t.Directors.GetEnumerator();
            while(directorIt.MoveNext())
            {
                var name = $"Director{directorIn}";
                commandText.AppendLine($"INSERT INTO MovieDirector (Id, Director) VALUES (@Id, @{name}");
                SetParam(command, name, directorIt.Current.Director);
                directorIn++;
            }

            var writerIn = 0;
            var writerIt = t.Writers.GetEnumerator();
            while(writerIt.MoveNext())
            {
                var name = $"Writer{writerIn}";
                commandText.AppendLine($"INSERT INTO MovieWriter (Id, Writer) VALUES (@Id, @{name}");
                SetParam(command, name, writerIt.Current.Writer);
                writerIn++;
            }

            var actorIn = 0;
            var actorIt = t.Actors.GetEnumerator();
            while (actorIt.MoveNext())
            {
                var name = $"Actor{actorIn}";
                commandText.AppendLine($"INSERT INTO MovieActor (Id, Actor) VALUES (@Id, @{name}");
                SetParam(command, name, actorIt.Current.Actor);
                actorIn++;
            }

            var locationIn = 0;
            var locationIt = t.FormatLocations.GetEnumerator();
            while (locationIt.MoveNext())
            {
                var formatName = $"Format{locationIn}";
                var locationName = $"Location{locationIn}";
                commandText.AppendLine($"INSERT INTO MovieFormatLocation (Id, [Format], [Location]) VALUES (@Id, {formatName}, {locationName})");
                SetParam(command, formatName, locationIt.Current.Format);
                SetParam(command, locationName, locationIt.Current.Location);
                locationIn++;
            }

            command.CommandText = commandText.ToString();

            return command;
        }

        public IDbCommand GenerateUpdate(Movie original, Movie t)
        {
            throw new NotImplementedException();
        }

        private string ReadCommandText(string filename)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void SetParam(IDbCommand command, string name, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            command.Parameters.Add(p);
        }
    }
}
