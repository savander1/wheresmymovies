using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using wheresmymovies.Utils;

namespace wheresmymovies.Entities
{
    public class AzureMovie
    {
        [JsonProperty(PropertyName = "@search.action")]
		public string SearchAction {get;set;}
        [JsonProperty(PropertyName = "id")]
        public string Id {get;set;}
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "year")]
        public List<string> Year { get; set; }
        [JsonProperty(PropertyName = "rated")]
        public string Rated { get; set; }
        [JsonProperty(PropertyName = "released")]
        public string Released { get; set; }
        [JsonProperty(PropertyName = "runtime")]
        public string Runtime { get; set; }
        [JsonProperty(PropertyName = "genre")]
        public List<string> Genre { get; set; }
        [JsonProperty(PropertyName = "directors")]
        public List<string> Director { get; set; }
        [JsonProperty(PropertyName = "writers")]
        public List<string> Writer { get; set; }
        [JsonProperty(PropertyName = "actors")]
        public List<string> Actors { get; set; }
        [JsonProperty(PropertyName = "plot")]
        public string Plot { get; set; }
        [JsonProperty(PropertyName = "language")]
        public List<string> Language { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "thumburl")]
        public string ThumbImgUrl { get; set; }
        [JsonProperty(PropertyName = "fullurl")]
        public string FullImgUrl { get; set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        public AzureMovie(Movie movie, string searchAction)
        {
            SearchAction =  searchAction;
            Id = movie.Id;
            Title = movie.Title;
            Year = movie.Year.Select(x=> x.ToString()).ToList();
            Rated = movie.Rated;
            Released = movie.Released.ToString();
            Runtime = movie.Runtime.ToString();
            Genre = movie.Genre?.FirstOrDefault().SplitOnCommas();
            Director = movie.Director?.FirstOrDefault().SplitOnCommas();
            Writer = movie.Writer?.FirstOrDefault().SplitOnCommas();
            Actors = movie.Actors?.FirstOrDefault().SplitOnCommas();
            Plot = movie.Plot;
            Language = movie.Language?.FirstOrDefault().SplitOnCommas();
            Country = movie.Country;
            ThumbImgUrl = movie.ThumbImgUrl;
            FullImgUrl = movie.FullImgUrl;
            Location = movie.Location;
        }
    }
}