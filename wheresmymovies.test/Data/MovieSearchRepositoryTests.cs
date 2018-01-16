using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using wheresmymovies.Data.Client;
using wheresmymovies.Data;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using System.Net;
using Microsoft.Azure.Search.Models;
using System.Collections.Generic;

namespace wheresmymovies.test.Data
{
    [TestClass]
    public class MovieSearchRepositoryTests : BaseTest
    {
        private Mock<ISearchClient> _searchClient;
        private Mock<IInfoClient> _infoClient;
        private IMovieRepositoryAsync _movieRepo;
        private static DocumentIndexResult _indexResult = new DocumentIndexResult(new List<IndexingResult>());

        [TestInitialize]
        public void Initialize()
        {
            _searchClient = new Mock<ISearchClient>();
            _infoClient = new Mock<IInfoClient>();
            _movieRepo = new MovieRepositoryAsync(_searchClient.Object, _infoClient.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_SearchClientNull_ThrowsExceptions()
        {
            new MovieRepositoryAsync(null, _infoClient.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_InfoClientNull_ThrowsExceptions()
        {
            new MovieRepositoryAsync(_searchClient.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddMovie_MovieNull_ThrowsExceptionAsync()
        {
            await _movieRepo.AddAsync(null);
        }
   
        [TestMethod]
        public async Task AddMovie_ValidMovie_ReturnAsExpected()
        {
            const string movieId = "12345&*";
            _searchClient.Setup(x => x.AddAsync(It.Is<Movie>(m => m.Id == movieId)))
                         .Returns(Task.FromResult(_indexResult));

            var result = await _movieRepo.AddAsync(new Movie { Id = movieId });

            _searchClient.Verify(x => x.AddAsync(It.Is<Movie>(m => m.Id == movieId)), Times.Once);
            Assert.AreEqual((int)HttpStatusCode.OK, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteAsync_MovieIdNull_ThrowsExceptionAsync()
        {
            await _movieRepo.DeleteAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteAsync_MovieIdEmpty_ThrowsExceptionAsync()
        {
            await _movieRepo.DeleteAsync(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteAsync_MovieIdWhitespace_ThrowsExceptionAsync()
        {
            await _movieRepo.DeleteAsync("    ");
        }

        [TestMethod]
        public async Task DeleteAsync_ValidMovie_ReturnAsExpected()
        {
            const string movieId = "12345&*";
            _searchClient.Setup(x => x.DeleteAsync(movieId))
                         .Returns(Task.FromResult(_indexResult));

            var result = await _movieRepo.DeleteAsync(movieId);

            _searchClient.Verify(x => x.DeleteAsync(It.Is<string>(m => m == movieId)), Times.Once);
            Assert.AreEqual((int)HttpStatusCode.OK, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAsync_MovieIdNull_ThrowsExceptionAsync()
        {
            await _movieRepo.GetAsync((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAsync_MovieIdEmpty_ThrowsExceptionAsync()
        {
            await _movieRepo.GetAsync(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAsync_MovieIdWhitespace_ThrowsExceptionAsync()
        {
            await _movieRepo.GetAsync("    ");
        }

        [TestMethod]
        public async Task GetAsync_ValidIdPassed_ReturnsAsExpected()
        {
            const string movieId = "12345&*";
            _searchClient.Setup(x => x.GetAsync(It.Is<string>(s => s.Equals(movieId))))
                .Returns(Task.FromResult(new Movie { Id = movieId }));

            var result = await _movieRepo.GetAsync(movieId);

            _searchClient.Verify(x => x.GetAsync(It.Is<string>(s => s.Equals(movieId))), Times.Once);
            Assert.AreEqual(movieId, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Search_NullPassed_ThrowsExeption()
        {
            await _movieRepo.SearchAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task Search_ParametersEmpty_ThrowsExeption()
        {
            await CheckExeceptionMessageAsync(async () =>
            {
                await _movieRepo.SearchAsync(new Models.SearchParameters());
            }, "Invalid");
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task Search_ParametersOverPopulated_ThrowsExeption()
        {
            await CheckExeceptionMessageAsync(async () =>
            {
                await _movieRepo.SearchAsync(new Models.SearchParameters
                {
                    Title = "foo",
                    Id = "bar"
                });
            }, "Invalid");
            
        }

        [TestMethod]
        public async Task Search_ValidParameters_InfoClientCalled()
        {
            await _movieRepo.SearchAsync(new Models.SearchParameters { Id = "1" });

            _infoClient.Verify(x => x.SearchForMoviesAsync(It.Is<Models.SearchParameters>(p => p.Id.Equals("1"))));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Update_IdNull_ThrowsException()
        {
            await CheckExeceptionMessageAsync( async () => 
                {
                    await _movieRepo.UpdateAsync(null, new Movie());
                }, "id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Update_IdEmpty_ThrowsException()
        {
            await CheckExeceptionMessageAsync(async () =>
            {
                await _movieRepo.UpdateAsync("", new Movie());
            }, "id"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Update_IdWhiteSpace_ThrowsException()
        {
            await CheckExeceptionMessageAsync(async () =>
            {
                await _movieRepo.UpdateAsync(" ", new Movie());
            }, "id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Update_MovieNull_ThrowsException()
        {
            await CheckExeceptionMessageAsync(async () =>
            {
                await _movieRepo.UpdateAsync("1", null);
            }, "movie");
        }

        [TestMethod]
        public async Task Update_ValidMovie_ReturnAsExpected()
        {
            const string movieId = "12345&*";
            _searchClient.Setup(x => x.AddAsync(It.Is<Movie>(m => m.Id == movieId)))
                         .Returns(Task.FromResult(_indexResult));

            var result = await _movieRepo.UpdateAsync(movieId, new Movie { Id = movieId });

            _searchClient.Verify(x => x.AddAsync(It.Is<Movie>(m => m.Id == movieId)), Times.Once);
            Assert.AreEqual((int)HttpStatusCode.OK, result);
        }

    }
}