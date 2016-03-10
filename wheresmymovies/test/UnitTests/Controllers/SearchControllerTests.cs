using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using wheresmymovies.Controllers;
using wheresmymovies.Data;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace test.UnitTests.Controllers
{
    public class SearchControllerTests
    {
        private Mock<IMovieRepository> _movieRepo;
        private SearchController _searchController;

        public SearchControllerTests()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _movieRepo.Setup(x => x.Get(It.Is<MovieSearchParameters>(search => search.Id == Constants.Id || search.Title == Constants.Title)))
                      .ReturnsAsync(new List<Movie> { new Movie { Title = Constants.Title, Id = Constants.Id } });
            _movieRepo.Setup(x => x.Get(It.Is<string>(search => search == Constants.Id)))
                      .ReturnsAsync(new Movie { Title = Constants.Title, Id = Constants.Id });
            _searchController = new SearchController(_movieRepo.Object);
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public void Ctr_MovieRepoNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _searchController = new SearchController(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_ValidIdPassed_MovieReturned()
        {
            var result = _searchController.Get(new MovieSearchParameters { Id = Constants.Id }).Result;

            _movieRepo.Verify(x => x.Get(It.Is<MovieSearchParameters>(search => search.Id == Constants.Id)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = ((IEnumerable<Movie>)result.Value).FirstOrDefault();
            Assert.NotNull(movie);
            Assert.Equal(Constants.Id, movie.Id);
            Assert.Equal(Constants.Title, movie.Title);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_ValidTitlePassed_MovieReturned()
        {
            var result = _searchController.Get(new MovieSearchParameters { Title = Constants.Title }).Result;

            _movieRepo.Verify(x => x.Get(It.Is<MovieSearchParameters>(search => search.Title == Constants.Title)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = ((IEnumerable<Movie>)result.Value).FirstOrDefault();
            Assert.NotNull(movie);
            Assert.Equal(Constants.Id, movie.Id);
            Assert.Equal(Constants.Title, movie.Title);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_EmptyParametersPassed_ReturnsBadRequest()
        {
            var result = _searchController.Get(new MovieSearchParameters()).Result;
            
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_BothParametersPassed_ReturnsBadRequest()
        {
            var result = _searchController.Get(new MovieSearchParameters
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
            var result = _searchController.Get((MovieSearchParameters)null).Result;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_NoMatchingMovies_ReturnsNotFound()
        {
            var result = _searchController.Get(new MovieSearchParameters { Id = "x" }).Result;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetById_ValidIdPassed_MovieReturned()
        {
            var result = _searchController.Get(Constants.Id).Result;

            _movieRepo.Verify(x => x.Get(It.Is<string>(search => search == Constants.Id)), Times.Once);
            Assert.Equal(200, result.StatusCode);
            var movie = (Movie)result.Value;
            Assert.NotNull(movie);
            Assert.Equal(Constants.Id, movie.Id);
            Assert.Equal(Constants.Title, movie.Title);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetById_NullPassed_ReturnsBadRequest()
        {
            var result = _searchController.Get((string)null).Result;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetById_NoMatchingMovies_ReturnsNotFound()
        {
            var result = _searchController.Get("x").Result;

            Assert.Equal(404, result.StatusCode);
        }
    }
}
