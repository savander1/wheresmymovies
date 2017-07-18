using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wheresmymovies.Data.Client;

namespace wheresmymovies.test.Data.Client
{
    [TestClass]
    public class OmovieClientTests : BaseTest
    {
        private Mock<IHttpClient> _client = new Mock<IHttpClient>();
        private const string _url = "http://www.zzz.com";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Ctr_UrlNull_ThrowsExceptionAsync()
        {
            await CheckExeceptionMessageAsync(() =>
            {
                new OmovieClient(null, _client.Object);
            }, "omdbUrl");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Ctr_UrlEmpty_ThrowsExceptionAsync()
        {
            await CheckExeceptionMessageAsync(() =>
            {
                new OmovieClient("", _client.Object);
            }, "omdbUrl");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Ctr_UrlWhitespace_ThrowsExceptionAsync()
        {
            await CheckExeceptionMessageAsync(() =>
            {
                new OmovieClient(" ", _client.Object);
            }, "omdbUrl");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Ctr_ClientNull_ThrowsExceptionAsync()
        {
            await CheckExeceptionMessageAsync(() =>
            {
                new OmovieClient(_url, null);
            }, "client");
        }

    }
}
