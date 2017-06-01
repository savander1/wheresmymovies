using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using wheresmymovies.Data.Client;
using wheresmymovies.Data;

namespace wheresmymovies.test.Data
{
    [TestClass]
    public class MovieSearchRepositoryTests
    {
        private Mock<ISearchClient> _searchClient;
        private Mock<IInfoClient> _infoClient;

        [TestInitialize]
        public void Initialize()
        {
            _searchClient = new Mock<ISearchClient>();
            _infoClient = new Mock<IInfoClient>();
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
    }
}
