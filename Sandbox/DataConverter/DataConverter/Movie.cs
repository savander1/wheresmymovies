using System;
using System.Collections.Generic;
using System.Linq;

namespace wheresmymovies.Entities
{
    public class MovieTEMP
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbId { get; set; }
        public string type { get; set; }
        public bool Response { get; set; }
    }

    public class Movie
    {
        public string Id {get;set;}
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Director { get; set; }
        public List<string> Writer { get; set; }
        public List<string> Actors { get; set; }
        public string Plot { get; set; }
        public List<string> Language { get; set; }
        public string Country { get; set; }
        public string ThumbImgUrl { get; set; }
        public string FullImgUrl { get; set; }

        public static Movie ToMovie(MovieTEMP temp)
        {
            return new Movie
            {
                Id = temp.imdbId,
                Title = temp.Title,
                Year = temp.Year,
                Rated = temp.Rated,
                Released = temp.Released,
                Runtime = temp.Runtime,
                Genre = temp.Genre.SplitEx().ToList(),
                Director = temp.Director.SplitEx().ToList(),
                Writer = temp.Writer.SplitEx().ToList(),
                Actors = temp.Actors.SplitEx().ToList(),
                Plot = temp.Plot,
                Language = temp.Language.SplitEx().ToList(),
                Country = temp.Country,
                ThumbImgUrl = GetThumbImageUrl(temp.Poster),
                FullImgUrl = temp.Poster
            };
        }

        private static string GetThumbImageUrl(string poster)
        {
            return poster.Replace("SX300.jpg", "SX100.jpg");
        } 
    }

    static class MovieExtensions
    {
        public static IEnumerable<string> SplitEx(this string s)
        {
            return s.Split(",".ToCharArray()).Select(x=> x.Trim());
        }
    }
}
