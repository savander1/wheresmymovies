using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ISqlGenerator<Movie, int> _sqlGenerator;
        private readonly IEntityReader<Movie> _entityReader;
        private readonly string _connectionString;

        protected MovieRepository(string connectionString, ISqlGenerator<Movie, int> sqlGenerator, IEntityReader<Movie> reader)
        {
            _sqlGenerator = sqlGenerator;
            _entityReader = reader;
            _connectionString = connectionString;
        }
        public MovieRepository(string connectionString) : this(connectionString, new MovieSqlGenerator(), new MovieReader()) { }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.Connection = conn;
                var command = _sqlGenerator.GenerateDelete(id);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Movie> Find(IQuery<Movie> query)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.Connection = conn;
                var command = _sqlGenerator.GenerateFind(query);
                using (var reader = command.ExecuteReader())
                {
                    return _entityReader.ReadList(reader);
                }
            }
        }

        public Movie Get(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.Connection = conn;
                var command = _sqlGenerator.GenerateGet(id);
                using (var reader = command.ExecuteReader())
                {
                    return _entityReader.Read(reader);
                }
            }
        }

        public Movie Save(Movie t)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.Connection = conn;
                var command = _sqlGenerator.GenerateSave(t);
                int id = (int)command.ExecuteScalar();
                var returnCommand = _sqlGenerator.GenerateGet(id);
                using (var reader = returnCommand.ExecuteReader())
                {
                    return _entityReader.Read(reader);
                }
            }
        }

        public Movie Update(int id, Movie t)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.Connection = conn;
                var command = _sqlGenerator.GenerateGet(id);
                Movie original;
                using (var reader = command.ExecuteReader())
                {
                    original = _entityReader.Read(reader);
                }
                var updateCmd = _sqlGenerator.GenerateUpdate(original, t);
                using (var reader = updateCmd.ExecuteReader())
                {
                    return _entityReader.Read(reader);
                }
            }
        }
    }
}
