using System;
using Moq;
using Xunit;
using wheresmymovies.Controllers;
using wheresmymovies.Data;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace test.UnitTests.Controllers
{
    public class MoviesControllerTests
    {
        private Mock<IMovieRepository> _movieRepo;
        private Mock<IMetaDataSearchRepository> _metaDataRepo;
        private MoviesController _moviesController;

        public MoviesControllerTests()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _metaDataRepo = new Mock<IMetaDataSearchRepository>();

            _metaDataRepo.Setup(x => x.Search(It.Is<MovieSearchParameters>(search => search.Id == Constants.Id || search.Title == Constants.Title)))
                      .ReturnsAsync(new Movie { Title = Constants.Title, Id = Constants.Id });

            _moviesController = new MoviesController(_movieRepo.Object, _metaDataRepo.Object);
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public void Ctr_MovieRepoNull_Throws()
        {
             _metaDataRepo = new Mock<IMetaDataSearchRepository>();
             
             Assert.Throws<ArgumentNullException>(() => _moviesController = new MoviesController(null, _metaDataRepo.Object));
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public void Ctr_MovieDataRepoNull_Throws()
        {
             _movieRepo = new Mock<IMovieRepository>();
             
             Assert.Throws<ArgumentNullException>(() => _moviesController = new MoviesController(_movieRepo.Object, null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_ValidIdPassed_MovieReturned()
        {
            var result = _moviesController.Get(new MovieSearchParameters { Id = Constants.Id }).Result;

            _metaDataRepo.Verify(x => x.Search(It.Is<MovieSearchParameters>(search => search.Id == Constants.Id)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(Constants.Id, movie.Id);
            Assert.Equal(Constants.Title, movie.Title);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_ValidTitlePassed_MovieReturned()
        {
            var result = _moviesController.Get(new MovieSearchParameters { Title = Constants.Title }).Result;

            _metaDataRepo.Verify(x => x.Search(It.Is<MovieSearchParameters>(search => search.Title == Constants.Title)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(Constants.Id, movie.Id);
            Assert.Equal(Constants.Title, movie.Title);
        }

        [Fact]
        public void Get_EmptyParametersPassed_ReturnsBadRequest()
        {
            var result = _moviesController.Get(new MovieSearchParameters()).Result;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_BothParametersPassed_ReturnsBadRequest()
        {
            var result = _moviesController.Get(new MovieSearchParameters
            {
                Title = "Foo",
                Id = "Bar"
            }).Result;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_Null_ReturnsBadRequest()
        {
            var result = _moviesController.Get(null).Result;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_NoMatchingMovies_ReturnsNotFound()
        {
            var result = _moviesController.Get(new MovieSearchParameters { Id = "x" }).Result;

            Assert.Equal(404, result.StatusCode);
        }

    }
}
