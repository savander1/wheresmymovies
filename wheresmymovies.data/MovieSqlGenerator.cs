using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class MovieSqlGenerator : ISqlGenerator<Movie, int>
    {
        private IDbConnection _connection;
        public IDbConnection Connection { set => _connection = value; }

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
            var commandText = new StringBuilder("UPDATE Movie \r\n");
            var command = _connection.CreateCommand();
            var updated = false;

            
            SetParam(command, "ThumbImgUrl", t.ThumbImgUrl);
            SetParam(command, "Description", t.Description);
            SetParam(command, "Runtime", t.Runtime);
            SetParam(command, "FullImgUrl", t.FullImgUrl);

            if (!original.Title.Equals(t.Title))
            {
                commandText.AppendLine("SET [Title] = @Title");
                SetParam(command, "Title", t.Title);
                updated = true;
            }

            if (!original.ThumbImgUrl.Equals(t.ThumbImgUrl))
            {
                commandText.AppendLine("SET [ThumbImgUrl] = @ThumbImgUrl");
                SetParam(command, "ThumbImgUrl", t.ThumbImgUrl);
                updated = true;
            }

            if (!original.Description.Equals(t.Description))
            {
                commandText.AppendLine("SET [Description] = @Description");
                SetParam(command, "Description", t.Description);
                updated = true;
            }

            if (!original.Runtime.Equals(t.Runtime))
            {
                commandText.AppendLine("SET [Runtime] = @Runtime");
                SetParam(command, "Runtime", t.Runtime);
                updated = true;
            }

            if (!original.FullImgUrl.Equals(t.FullImgUrl))
            {
                commandText.AppendLine("SET [FullImgUrl] = @FullImgUrl");
                SetParam(command, "FullImgUrl", t.FullImgUrl);
                updated = true;
            }

            if (updated)
            {
                commandText.AppendLine(" WHERE Id = @Id");
            }

            if (!AreEqual(original.Years, t.Years))
            {
                var toRemove = original.Years.Except(t.Years);
                var toAdd = t.Years.Except(original.Years);

                var index = 0;
                foreach(var year in toRemove)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"DELETE FROM MovieYear WHERE Id = @Id AND [Year] = @{name}");
                    SetParam(command, name, year.Year);
                    index++;
                }
                foreach(var year in toAdd)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"INSERT INTO MovieYear (Id, [Year]) VALUES (@Id, @{name})");
                    SetParam(command, name, year.Year);
                    index++;
                }
            }

            if (!AreEqual(original.Genres, t.Genres))
            {
                var toRemove = original.Genres.Except(t.Genres);
                var toAdd = t.Genres.Except(original.Genres);

                var index = 0;
                foreach (var genre in toRemove)
                {
                    var name = $"Genre{index}";
                    commandText.AppendLine($"DELETE FROM MovieGenre WHERE Id = @Id AND [Genre] = @{name}");
                    SetParam(command, name, genre.Genre);
                    index++;
                }
                foreach (var genre in toAdd)
                {
                    var name = $"Genre{index}";
                    commandText.AppendLine($"INSERT INTO MovieGenre (Id, [Genre]) VALUES (@Id, @{name})");
                    SetParam(command, name, genre.Genre);
                    index++;
                }
            }

            if (!AreEqual(original.Directors, t.Directors))
            {
                var toRemove = original.Directors.Except(t.Directors);
                var toAdd = t.Directors.Except(original.Directors);

                var index = 0;
                foreach (var director in toRemove)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"DELETE FROM MovieDirector WHERE Id = @Id AND [Director] = @{name}");
                    SetParam(command, name, director.Director);
                    index++;
                }
                foreach (var director in toAdd)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"INSERT INTO MovieDirector (Id, [Director]) VALUES (@Id, @{name})");
                    SetParam(command, name, director.Director);
                    index++;
                }
            }

            if (!AreEqual(original.Writers, t.Writers))
            {
                var toRemove = original.Writers.Except(t.Writers);
                var toAdd = t.Writers.Except(original.Writers);

                var index = 0;
                foreach (var writer in toRemove)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"DELETE FROM MovieWriter WHERE Id = @Id AND [Writer] = @{name}");
                    SetParam(command, name, writer.Writer);
                    index++;
                }
                foreach (var writer in toAdd)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"INSERT INTO MovieWriter (Id, [Writer]) VALUES (@Id, @{name})");
                    SetParam(command, name, writer.Writer);
                    index++;
                }
            }

            if (!AreEqual(original.Actors, t.Actors))
            {
                var toRemove = original.Actors.Except(t.Actors);
                var toAdd = t.Actors.Except(original.Actors);

                var index = 0;
                foreach (var actor in toRemove)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"DELETE FROM MovieActor WHERE Id = @Id AND [Actor] = @{name}");
                    SetParam(command, name, actor.Actor);
                    index++;
                }
                foreach (var actor in toAdd)
                {
                    var name = $"Year{index}";
                    commandText.AppendLine($"INSERT INTO MovieActor (Id, [Actor]) VALUES (@Id, @{name})");
                    SetParam(command, name, actor.Actor);
                    index++;
                }
            }

            if (!AreEqual(original.FormatLocations, t.FormatLocations))
            {
                var toRemove = original.FormatLocations.Except(t.FormatLocations);
                var toAdd = t.FormatLocations.Except(original.FormatLocations);

                var index = 0;
                foreach (var formatLocation in toRemove)
                {
                    var formatName = $"Format{index}";
                    var locationName = $"Location{index}";
                    commandText.AppendLine($"DELETE FROM MovieFormatLocation WHERE Id = @Id AND [Format] = @{formatName} AND [Location] = @{locationName}");
                    SetParam(command, formatName, formatLocation.Format);
                    SetParam(command, locationName, formatLocation.Location);
                    index++;
                }
                foreach (var formatLocation in toAdd)
                {
                    var formatName = $"Format{index}";
                    var locationName = $"Location{index}";
                    commandText.AppendLine($"INSERT INTO MovieFormatLocation (Id, [Format], [Location]) VALUES (@Id, @{formatName}, @{locationName})");
                    SetParam(command, formatName, formatLocation.Format);
                    SetParam(command, locationName, formatLocation.Location);
                    index++;
                }
            }

            return command;
        }

        private string ReadCommandText(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resource = assembly.GetManifestResourceNames().Single(x => x.Contains(filename));
            using (var stream = assembly.GetManifestResourceStream(resource))
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

        private static bool AreEqual<T>(ICollection<T> original, ICollection<T> t)
        {
            if (original.Count != t.Count) return false;

            if (original.Except(t).Any()) return false;

            if (t.Except(original).Any()) return false;

            return true;
        }
    }
}
