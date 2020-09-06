using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class Repository<T, TId> : IRepository<T, TId>
    {
        private readonly ISqlGenerator<T, TId> _sqlGenerator;
        private readonly IEntityReader<T> _entityReader;
        private readonly string _connectionString;

        protected Repository(string connectionString, ISqlGeneratorFactory sqlFactory, IEntityReaderFactory readerFactory)
        {
            _sqlGenerator = sqlFactory.GetSqlGenerator<T, TId>();
            _entityReader = readerFactory.GetReader<T>();
            _connectionString = connectionString;
        }
        public Repository(string connectionString) : this(connectionString, new SqlGeneratorFactory(), new EntityReaderFactory()) { }

        public void Delete(TId id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.connection = conn;
                var command = _sqlGenerator.GenerateDelete(id);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> Find(IQuery<T> query)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.connection = conn;
                var command = _sqlGenerator.GenerateFind(query);
                using (var reader = command.ExecuteReader())
                {
                    return _entityReader.ReadList(reader);
                }
            }
        }

        public T Get(TId id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.connection = conn;
                var command = _sqlGenerator.GenerateGet(id);
                using (var reader = command.ExecuteReader())
                {
                    return _entityReader.Read(reader);
                }
            }
        }

        public T Save(T t)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.connection = conn;
                var command = _sqlGenerator.GenerateSave(t);
                TId id = (TId)command.ExecuteScalar();
                var returnCommand = _sqlGenerator.GenerateGet(id);
                using (var reader = returnCommand.ExecuteReader())
                {
                    return _entityReader.Read(reader);
                }
            }
        }

        public T Update(TId id, T t)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                conn.Open();
                _sqlGenerator.connection = conn;
                var command = _sqlGenerator.GenerateGet(id);
                T original = default(T);
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
