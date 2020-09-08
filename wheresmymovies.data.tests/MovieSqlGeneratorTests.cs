using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using wheresmymovies.data;
using wheresmymovies.entities;

namespace wheresmymovies.data.tests
{
    [TestClass]
    public class MovieSqlGeneratorTests
    {
        private Mock<IDbConnection> _connection;
        private MovieSqlGenerator generator;

        [TestInitialize]
        public void Init()
        {
            _connection = new Mock<IDbConnection>();
            _connection.Setup(x => x.CreateCommand()).Returns(new MyDbCommand());
            generator = new MovieSqlGenerator();
            generator.Connection = _connection.Object;

        }


        [TestMethod]
        public void GenerateSave_MoviePassed_SqlGenerated()
        {
            var toSave = new Movie
            {
                Title = "Movie Title",
                ThumbImgUrl = "https://example.com/thumb.img",
                Description = "This is a description of the movie.",
                Runtime = 144,
                FullImgUrl = "https://example.com/full.img",
                Years = new List<MovieYear>
                {
                    new MovieYear { Year = 1988 }
                }
            }; 

            var command = generator.GenerateSave(toSave);

            Assert.IsNotNull(command.CommandText);

        }
    }

    internal class MyDbCommand : IDbCommand
    {
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDbConnection Connection { get; set; }

        public IDataParameterCollection Parameters { get; set; } = new MyParamterCollection();

        public IDbTransaction Transaction { get; set; }
        public UpdateRowSource UpdatedRowSource { get; set; }

        public void Cancel()
        {
            
        }

        public IDbDataParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public void Dispose()
        {

        }

        public int ExecuteNonQuery()
        {
            return 0;
        }

        public IDataReader ExecuteReader()
        {
            return new Mock<IDataReader>().Object;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return new Mock<IDataReader>().Object;
        }

        public object ExecuteScalar()
        {
            return 0;
        }

        public void Prepare()
        {
            
        }
    }

    internal class MyParamterCollection : List<SqlParameter>, IDataParameterCollection
    {
        public object this[string parameterName] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool Contains(string parameterName)
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new System.NotImplementedException();
        }
    }
}
