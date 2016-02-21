using System.Collections.Generic;
using System.Linq;
using Moq;
using wheresmymovies.Data;
using wheresmymovies.Controllers;
using Xunit;
using wheresmymovies.Models;
using wheresmymovies.Entities;
using System.Net;

namespace test.UnitTests.Controllers
{
    public class MoviesControllerTests : ControllerTestBase
    {
        private Mock<IMovieRepository> _movieRepo;
        private Mock<IMetaDataSearchRepository> _metaDataRepo;
        private MoviesController _moviesController;

        protected override void TestInitialize()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _metaDataRepo = new Mock<IMetaDataSearchRepository>();

            _metaDataRepo.Setup(x => x.Search(It.Is<MovieSearchParameters>(search => search.Id == ID || search.Title == TITLE)))
                      .ReturnsAsync(new Movie { Title = TITLE, Id = ID });

            _moviesController = new MoviesController(_movieRepo.Object, _metaDataRepo.Object);
        }

        [Fact]
        public void Get_ValidIdPassed_MovieReturned()
        {
            TestInitialize();

            var result = _moviesController.Get(new MovieSearchParameters { Id = ID }).Result;

            _metaDataRepo.Verify(x => x.Search(It.Is<MovieSearchParameters>(search => search.Id == ID)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(ID, movie.Id);
            Assert.Equal(TITLE, movie.Title);
        }

        [Fact]
        public void Get_ValidTitlePassed_MovieReturned()
        {
            TestInitialize();

            var result = _moviesController.Get(new MovieSearchParameters { Title = TITLE }).Result;

            _metaDataRepo.Verify(x => x.Search(It.Is<MovieSearchParameters>(search => search.Title == TITLE)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(ID, movie.Id);
            Assert.Equal(TITLE, movie.Title);
        }

        [Fact]
        public void Get_EmptyParametersPassed_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _moviesController.Get(new MovieSearchParameters()).Result;

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_BothParametersPassed_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _moviesController.Get(new MovieSearchParameters
            {
                Title = "Foo",
                Id = "Bar"
            }).Result;

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_Null_ReturnsBadRequest()
        {
            TestInitialize();

            var result = _moviesController.Get(null).Result;

            Assert.Equal(BAD_REQUEST, result.StatusCode);
        }

        [Fact]
        public void Get_NoMatchingMovies_ReturnsNotFound()
        {
            TestInitialize();

            var result = _moviesController.Get(new MovieSearchParameters { Id = "x" }).Result;

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}
