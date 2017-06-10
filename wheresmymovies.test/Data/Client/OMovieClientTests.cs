using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using wheresmymovies.Data.Client;

namespace wheresmymovies.test.Data.Client
{
    [TestClass]
    public class OmovieClientTests
    {
        private Mock<IHttpClient> _client = new Mock<IHttpClient>();
        private const string _url = "http://www.zzz.com";

        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "omdbUrl")]
        public void Ctr_UrlNull_ThrowsException()
        {
            new OmovieClient(null, _client.Object);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "omdbUrl")]
        public void Ctr_UrlEmpty_ThrowsException()
        {
            new OmovieClient("", _client.Object);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "omdbUrl")]
        public void Ctr_UrlWhitespace_ThrowsException()
        {
            new OmovieClient(" ", _client.Object);
        }

        [TestMethod]
        [ExpectedExceptionMessage(typeof(ArgumentNullException), "client")]
        public void Ctr_ClientNull_ThrowsException()
        {
            new OmovieClient(_url, null);
        }

    }
}
