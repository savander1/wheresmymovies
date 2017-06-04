using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using wheresmymovies.Utils;

namespace wheresmymovies.test.Utils
{
    [TestClass]
    public class StringExtensionTests : BaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetYear_StringIsNonNumeric_ThrowsException()
        {
            const string year = "abcd";
            year.GetYear();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidYearException))]
        public void GetYear_YearIsInvalid_ThrowsException()
        {
            const string year = "0";
            year.GetYear();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetYear_InvalidStartOfRange_ThrowsException()
        {
            const string year = "a-1977";
            year.GetYear();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetYear_InvalidEndOfRange_ThrowsException()
        {
            const string year = "1977-a";
            year.GetYear();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidYearException))]
        public void GetYear_StartYearOutOfRange_ThrowsException()
        {
            const string year = "0-1977";
            year.GetYear();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidYearException))]
        public void GetYear_EndYearOutOfRange_ThrowsException()
        {
            const string year = "1977-3333";
            year.GetYear();
        }

        [TestMethod]
        public void GetYear_StringIsNumeric_ReturnsYear()
        {
            const string year = "1978";

            var result = year.GetYear().FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.AreEqual(1978, result);
        }

        [TestMethod]
        public void GetYear_ValidRange_ReturnsYears()
        {
            const string year = "1980-1990";

            var result = year.GetYear();

            var index = 0;
            for (var i = 1980; i <= 1990; i++)
            {
                Assert.AreEqual(i, result[index]);
                index++;
            }
        }

        [TestMethod]
        public void SplitOnCommas_CommaDelimitedList_ListReturnedAsExpected()
        {
            var list = "dog,     cat,fish       ,   goat   ";

            var result = list.SplitOnCommas();

            Assert.IsTrue(result.Contains("dog"));
            Assert.IsTrue(result.Contains("cat"));
            Assert.IsTrue(result.Contains("fish"));
            Assert.IsTrue(result.Contains("goat"));
        }
    }
}
