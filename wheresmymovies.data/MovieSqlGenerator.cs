using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class MovieSqlGenerator : ISqlGenerator<Movie, int>
    {
        private IDbConnection _connection;
        public IDbConnection connection { set => _connection = value; }

        public IDbCommand GenerateDelete(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"DELETE FROM Movie WHERE Id = @Id";
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
            throw new NotImplementedException();
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
    }
}
