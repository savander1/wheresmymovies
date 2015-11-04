using System;
using System.Collections.Generic;
using wheresmymovies.Utils;

namespace wheresmymovies.Entities
{
    public class AzureMovie
    {
		public string SearchAction {get;set;}
        public string Id {get;set;}
        public string Title { get; set; }
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

        public AzureMovie(Movie movie, string searchAction)
        {
            SearchAction =  searchAction;
            Id = movie.imdbId;
            Title = movie.Title;
            Year = movie.Year.GetYear();
            Rated = movie.Rated;
            Released = movie.Released.GetReleaseDate();
            Runtime = movie.Runtime.GetRuntime();
            Genre = movie.Genre.SplitOnCommas();
            Director = movie.Director.SplitOnCommas();
            Writer = movie.Writer.SplitOnCommas();
            Actors = movie.Actors.SplitOnCommas();
            Plot = movie.Plot;
            Language = movie.Language.SplitOnCommas();
            Country = movie.Country;
            ThumbImgUrl = movie.Poster.GetThumbImageUrl();
            FullImgUrl = movie.Poster;
            Location = string.Empty;
        }
    }