using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using wheresmymovies.Data;
using wheresmymovies.Data.Client;

namespace wheresmymovies.test.Data.Client
{
    [TestClass]
    public class AzureSearchClientTests : BaseTest
    {
        private static AzureSearchConfiguration _configuration = new AzureSearchConfiguration(Uuid(), Uuid());

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_ConfigurationIsNull_ThrowsException()
        {
            new AzureSearchClient(null);
        }

        [TestMethod]
        public void Ctr_ValidConfiguration_AzureConfiguredAsExpected()
        {
            var client = new AzureSearchClient(_configuration);
            Assert.AreEqual(_configuration.ApiKey, client.Azure.SearchCredentials.ApiKey);
        }

        [TestMethod]
        public void Ctr_ValidConfiguration_AdminConfiguredAsExpected()
        {
            var client = new AzureSearchClient(_configuration);
            Assert.AreEqual(_configuration.AdminApiKey, client.Admin.SearchCredentials.ApiKey);
        }
    }
}
