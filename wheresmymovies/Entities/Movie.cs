using System;
using System.Collections.Generic;
using wheresmymovies.Utils;

namespace wheresmymovies.Entities
{
    public class Movie
    {
        public string Id {get;set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> Year { get; set; }
        public string Rated { get; set; }
        public DateTime Released { get; set; }
        public TimeSpan Runtime { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Director { get; set; }
        public List<string> Writer { get; set; }
        public List<string> Actors { get; set; }
        public string Plot { get; set; }
        public List<string> Language { get; set; }
        public string Country { get; set; }
        public string ThumbImgUrl { get; set; }
        public string FullImgUrl { get; set; }
        public string Location { get; set; }

        public Movie () { }

        //public Movie(Omovie oMovie)
        //{
        //    Id = oMovie.imdbId;
        //    Title = oMovie.Title;
        //    Year = oMovie.Year.GetYear();
        //    Rated = oMovie.Rated;
        //    Released = oMovie.Released.GetReleaseDate();
        //    Runtime = oMovie.Runtime.GetRuntime();
        //    Genre = oMovie.Genre.SplitOnCommas();
        //    Director = oMovie.Director.SplitOnCommas();
        //    Writer = oMovie.Writer.SplitOnCommas();
        //    Actors = oMovie.Actors.SplitOnCommas();
        //    Plot = oMovie.Plot;
        //    Language = oMovie.Language.SplitOnCommas();
        //    Country = oMovie.Country;
        //    ThumbImgUrl = oMovie.Poster?.GetThumbImageUrl();
        //    FullImgUrl = oMovie.Poster;
        //    Location = null;
        //}
    }
}
