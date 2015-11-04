using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using wheresmymovies.Utils;

namespace wheresmymovies.Entities
{
    public class AzureMovie
    {
        [JsonProperty(PropertyName = "@search.action")]
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
            Id = movie.Id;
            Title = movie.Title;
            Year = movie.Year;
            Rated = movie.Rated;
            Released = movie.Released;
            Runtime = movie.Runtime;
            Genre = movie.Genre;
            Director = movie.Director;
            Writer = movie.Writer;
            Actors = movie.Actors;
            Plot = movie.Plot;
            Language = movie.Language;
            Country = movie.Country;
            ThumbImgUrl = movie.ThumbImgUrl;
            FullImgUrl = movie.FullImgUrl;
            Location = movie.Location;
        }
    }
}