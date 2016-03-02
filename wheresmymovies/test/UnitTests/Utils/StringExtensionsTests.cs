using System;
using System.Collections.Generic;
using System.Linq;
using wheresmymovies.Utils;
using Xunit;

namespace test.UnitTests.Utils
{
    public class StringExtensionTests
    {
        [Fact]
        public void GetYear_SingleYear_YearReturned()
        {
            var year = "1990".GetYear().Single();
            
            Assert.Equal(1990, year);     
        } 
        
        [Fact]
        public void GetYear_Range_YearsReturned()
        {
            var years = "1990-2000".GetYear();
            
            var expected = new List<int> {
                1990,
                1991,
                1992,
                1993,
                1994,
                1995,
                1996,
                1997,
                1998,
                1999,
                2000
            };
            
            Assert.Equal(expected, years);     
        } 
        
        [Fact]
        public void GetYear_OpenRange_YearsReturned()
        {
            var expected = new List<int>();
            var currentYear = DateTime.Now.Year;
            for (var i = 2014; i <= currentYear; i++){
                expected.Add(i);
            }
            
            var years = "2014-".GetYear();

            Assert.Equal(expected, years);     
        } 
        
        [Fact]
        public void GetYear_StringPassed_EmptyListReturned()
        {
            var expected = new List<int>();
  
            var years = "year".GetYear();

            Assert.Equal(expected, years);     
        }
        
        [Fact]
        public void GetYear_SentencePassed_EmptyListReturned()
        {
            var expected = new List<int>();
  
            var years = "not a year".GetYear();

            Assert.Equal(expected, years);     
        }
        
         [Fact]
        public void GetYear_MixedString_EmptyListReturned()
        {
            var expected = new List<int>();
            var currentYear = DateTime.Now.Year;
            for (var i = 2014; i <= currentYear; i++){
                expected.Add(i);
            }
  
            var years = "2014 was a year".GetYear();

            Assert.Equal(expected, years);     
        }
        
        [Fact]
        public void GetYear_MixedYearInMiddleString_EmptyListReturned()
        {
            var expected = new List<int>();
            
            var years = "I like 2014".GetYear();

            Assert.Equal(expected, years);     
        }
        
        [Fact]
        public void GetYear_EmptyString_EmptyListReturned()
        {
            var expected = new List<int>();
  
            var years = "".GetYear();

            Assert.Equal(expected, years);     
        }
        
        [Fact]
        public void GetReleaseDate_DatePassed_DateReturned()
        {
            var expected = new DateTime(2014, 12, 25);
            
            var actual = "25 Dec 2014".GetReleaseDate();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetReleaseDate_NaString_MinDateReturned()
        {
            var result = "N/A".GetReleaseDate();
            
            Assert.Equal(DateTime.MinValue, result);
        }
        
        [Fact]
        public void GetReleaseDate_EmptyString_Throws()
        {
            Assert.Throws<FormatException>(() => "".GetReleaseDate());
        }
        
        [Fact]
        public void GetReleaseDate_StringPassed_Throws()
        {           
            Assert.Throws<FormatException>(() => "This is not a date".GetReleaseDate());
        }
        
        [Fact]
        public void GetRuntime_TimePassed_TimeSpanReturned()
        {
            var expected = new TimeSpan(1,4,0);
            
            var actual = "64 minutes".GetRuntime();
            
            Assert.Equal(expected, actual);
        }
        
       
        [Fact]
        public void GetRuntime_EmptyString_EmptyTimespanReturned()
        {
            var expected = new TimeSpan();
            
            var actual = "".GetRuntime();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetRuntime_StringPassed_Throws()
        {       
            var expected = new TimeSpan();
            
            var actual = "This is not a date".GetRuntime();
            
            Assert.Equal(expected, actual);    
        }
        
        [Fact]
        public void SplitOnCommas_CSVPassed_ListReturned()
        {
            var expected = new List<string>{"a", "b", "c", "d"};
            
            var actual = "a,b,c,d".SplitOnCommas();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SplitOnCommas_EmptyString_EmptyListReturned()
        {
            var expected = new List<string>();
            
            var actual = "".SplitOnCommas();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetThumbImageUrl_FullSizePassed_ThumbnailReturned()
        {
            var expected = "SX100.jpg";
            
            var actual = "SX300.jpg".GetThumbImageUrl();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetThumbImageUrl_StringPassed_StringReturned()
        {
            var expected = "bob";
            
            var actual = "bob".GetThumbImageUrl();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetThumbImageUrl_EmptyStringPassed_EmptyStringReturned()
        {
            var expected = "";
            
            var actual = "".GetThumbImageUrl();
            
            Assert.Equal(expected, actual);
        }
    }
}