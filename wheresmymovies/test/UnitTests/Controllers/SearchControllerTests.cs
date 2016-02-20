using System.Collections.Generic;
using Moq;
using wheresmymovies.Data;
using Xunit;
using wheresmymovies.Controllers;
using wheresmymovies.Models;
using wheresmymovies.Entities;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace test.UnitTests.Controllers
{
    public class SearchControllerTests
    {
        private Mock<IMovieRepository> _movieRepo;
        private SearchController _searchController;
        private const string ID = "tt123456789";
        private const string TITLE = "Marvelous Movie";
        private const int BAD_REQUEST = 400;

        public void TestInitialize()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _movieRepo.Setup(x => x.Get(It.Is<MovieSearchParameters>(search => search.Id == ID || search.Title == TITLE)))
                      .Returns(new List<Movie> { new Movie { Title = TITLE, Id = ID } });
            _movieRepo.Setup(x => x.Get(It.Is<string>(search => search == ID)))
                      .ReturnsAsync(new Movie { Title = TITLE, Id = ID });
            _searchController = new SearchController(_movieRepo.Object);
        }

        [Fact]
        public void Get_ValidIdPassed_MovieRepoCalledAsExpected()
        {
            TestInitialize();

            var result = _searchController.Get(new MovieSearchParameters { Id = ID });

            _movieRepo.Verify(x => x.Get(It.Is<MovieSearchParameters>(search => search.Id == ID)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = ((IEnumerable<Movie>)result.Value).FirstOrDefault();
            Assert.NotNull(movie);
            Assert.Equal(ID, movie.Id);
            Assert.Equal(TITLE, movie.Title);
        }

        [Fact]
        public void Get_ValidIdTitlePassed_MovieRepoCalledAsExpected()
        {
            TestInitialize();

            _searchController.Get(new MovieSearchParameters { Title = TITLE });

            _movieRepo.Verify(x => x.Get(It.Is<MovieSearchParameters>(search => search.Title == TITLE)), Times.Once);
        }

        [Fact]
        public void Get_EmptyParametersPassed_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _searchController.Get(new MovieSearchParameters());
            
            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_BothParametersPassed_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _searchController.Get(new MovieSearchParameters
            {
                Title = "Foo",
                Id = "Bar"
            });

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_Null_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _searchController.Get((MovieSearchParameters)null);

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_NoMatchingMovies_ReturnsNotFound()
        {
            TestInitialize();

            var result = _searchController.Get(new MovieSearchParameters { Id = "x" });

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void GetById_ValidIdPassed_MovieReturned()
        {
            TestInitialize();

            var result = _searchController.Get(ID).Result;

            _movieRepo.Verify(x => x.Get(It.Is<string>(search => search == ID)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(ID, movie.Id);
            Assert.Equal(TITLE, movie.Title);
        }

        [Fact]
        public void GetById_NullPassed_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _searchController.Get((string)null).Result;

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void GetById_NoMatchingMovies_ReturnsNotFound()
        {
            TestInitialize();

            var result = _searchController.Get("x").Result;

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
