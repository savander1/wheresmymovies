using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wheresmymovies.Data;
using Moq;
using wheresmymovies.Services;
using wheresmymovies.Entities;

namespace wheresmymovies.test
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _movieRepo;
        private Mock<IMetaDataSearchRepository> _metaRepo;
        private MovieServiceAsync _movieService;

        [TestInitialize]
        public void Setup()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _metaRepo = new Mock<IMetaDataSearchRepository>();

            _movieService = new MovieServiceAsync(_movieRepo.Object, _metaRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_MovieRepoNull_Throws()
        {
            _movieService = new MovieServiceAsync(null, _metaRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_MetaRepoNull_Throws()
        {
            _movieService = new MovieServiceAsync(_movieRepo.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async void AddMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.AddMovie(null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddMovie_ValidMovie_MovieAddedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.AddMovie(movie);

            _movieRepo.Verify(x => x.Add(It.Is<Movie>(m => m.Id == movieId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task DeleteMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.DeleteMovie(null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteMovie_ValidMovie_MovieDeletedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.DeleteMovie(movie);

            _movieRepo.Verify(x => x.Delete(It.Is<string>(m => m == movieId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async System.Threading.Tasks.Task UpdateMovie_MovieNull_ThrowsAsync()
        {
            await _movieService.UpdateMovie(null);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateMovieMovie_ValidMovie_MovieDeletedAsync()
        {
            var movieId = Guid.NewGuid().ToString();
            var movie = new Movie
            {
                Id = movieId
            };

            await _movieService.UpdateMovie(movie);

            _movieRepo.Verify(x => x.Update(It.Is<string>(m => m == movieId), It.Is<Movie>(m => m.Id == movieId)), Times.Once);
        }
    }
}
