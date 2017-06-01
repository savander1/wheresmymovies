using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace wheresmymovies.Utils
{
    public static class StringExtensions
    {
        public static List<int> GetYear(this string s)
        {
            var regex = new Regex("[^\\d]+");
            if (regex.IsMatch(s))
            {
                return GetYearRange(s, regex.Match(s).Value);
            }

            int year;
            if (int.TryParse(s, out year))
                return new List<int> { year };

            return new List<int>();
        }

        private static List<int> GetYearRange(string s, string delimiter)
        {
            var yearParts = s.Split(delimiter.ToCharArray()).ToList();
            int start;
            if (!int.TryParse(yearParts.First(), out start))
            {
                return new List<int>();
            }

            var second = yearParts.Last();
            var end = string.IsNullOrEmpty(second)
                ? DateTime.Now.Year
                : int.Parse(second);

            var range = new List<int>();
            for (var i = start; i <= end; i++)
            {
                range.Add(i);
            }

            return range;
        }

        public static DateTime GetReleaseDate(this string s)
        {
            if (s.Equals("N/A")) return DateTime.MinValue;
            return DateTime.ParseExact(s, "dd MMM yyyy", CultureInfo.InvariantCulture);
        }

        public static TimeSpan GetRuntime(this string s)
        {
            var regex = new Regex("\\d+");
            var time = regex.Match(s).Value;
            if (string.IsNullOrEmpty(time)) return new TimeSpan();
            var minutes = int.Parse(time);

            return TimeSpan.FromMinutes(minutes);
        }

        public static List<string> SplitOnCommas(this string s)
        {
            return s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();
        }

        public static string GetThumbImageUrl(this string poster)
        {
            return poster.Replace("SX300.jpg", "SX100.jpg");
        }
    }
}
