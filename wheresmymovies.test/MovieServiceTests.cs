using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wheresmymovies.Data;
using Moq;
using wheresmymovies.Services;

namespace wheresmymovies.test
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _movieRepo;
        private Mock<IMetaDataSearchRepository> _metaRepo;
        private MovieService _movieService;

        [TestInitialize]
        public void Setup()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _metaRepo = new Mock<IMetaDataSearchRepository>();

            _movieService = new MovieService(_movieRepo.Object, _metaRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_MovieRepoNull_Throws()
        {
            _movieService = new MovieService(null, _metaRepo.Object);
        }
    }
}
