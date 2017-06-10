using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wheresmymovies.Data;
using Moq;
using wheresmymovies.Services;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace wheresmymovies.test.Services
{
    [TestClass]
    public class MovieServiceTests : BaseTest
    {
        private Mock<IMovieRepositoryAsync> _movieRepo;
        private MovieServiceAsync _movieService;
        private const string Id = "1234";
        private const string Title = "Expected";

        [TestInitialize]
        public void Setup()
        {
            _movieRepo = new Mock<IMovieRepositoryAsync>();

            _movieRepo.Setup(repo => repo.GetAsync(It.Is<SearchFilters>(f => f.Title == Title)))
                      .Returns(() => Task.Factory.StartNew(() => new List<Movie>
                      {
                         new Movie
                         {
                             Title = Title,
                             Id = Id
                         }
                      }));

            _movieRepo.Setup(repo => repo.SearchAsync(It.Is<SearchParameters>(p => p.Id == Id)))
                     .Returns(() => Task.Factory.StartNew(() => new Movie
                     {
                         Title = Title,
                         Id = Id
                     }));

            _movieService = new MovieServiceAsync(_movieRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_MovieRepoNull_Throws()
        {
            _movieService = new MovieServiceAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.AddMovie(null);
        }

        [TestMethod]
        public async Task AddMovie_ValidMovie_MovieAddedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.AddMovie(movie);

            _movieRepo.Verify(x => x.AddAsync(It.Is<Movie>(m => m.Id == movieId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.DeleteMovie(null);
        }

        [TestMethod]
        public async Task DeleteMovie_ValidMovie_MovieDeletedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.DeleteMovie(movie);

            _movieRepo.Verify(x => x.DeleteAsync(It.Is<string>(m => m == movieId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.UpdateMovie(null);
        }

        [TestMethod]
        public async Task UpdateMovieMovie_ValidMovie_MovieDeletedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.UpdateMovie(movie);

            _movieRepo.Verify(x => x.UpdateAsync(It.Is<string>(m => m == movieId), It.Is<Movie>(m => m.Id == movieId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task FetchMovieMetadata_NullPassed_ThrowsAsync()
        {
            await _movieService.FetchMovieMetadata(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSearchParametersException))]
        public async Task FetchMovieMetadata_InvalidSearchParameters_ThrowsAsync()
        {
            var invalidParams = new SearchParameters();
            await _movieService.FetchMovieMetadata(invalidParams);
        }

        [TestMethod]
        public async Task FetchMovieMetadata_ValidParametersPassed_DataReturnedAsync()
        {

            var validParams = new SearchParameters { Id = Id };

            var result = await _movieService.FetchMovieMetadata(validParams);

            Assert.IsNotNull(result);
            Assert.AreEqual(Title, result.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SearchAllMovies_NullPassed_ThrowsAsync()
        {
            await _movieService.SearchAllMovies(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSearchFilterException))]
        public async Task SearchAllMovies_InvalidParameters_ThrowsAsync()
        {
            var invalidParameters = new SearchFilters();
            await _movieService.SearchAllMovies(invalidParameters);
        }

        [TestMethod]
        public async Task SearchAllMovies_ValidParamters_ReturnsMoviesAsync()
        {
            var validSearchFilters = new SearchFilters
            {
                Title = Title
            };

            var result = await _movieService.SearchAllMovies(validSearchFilters);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(Title, result.Single().Title);
        }
    }
}
