using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wheresmymovies.Utils;

namespace wheresmymovies.test.Utils
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void GetYear_StringIsNonNumeric_ReturnsEmptyList()
        {
            const string year = "abcd";

            var result = year.GetYear();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetYear_StringIsNumeric_ReturnsYear()
        {
            const string year = "1978";

            var result = year.GetYear().FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(1978, result);
        }
    }
}
