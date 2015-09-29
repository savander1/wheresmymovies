using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wheresmymovies.Entities
{
    public class MovieTEMP
    {
        public string Title { get; set; }
        public int Year { get; set; }
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
        public string Awards { get; set; }
        public string Poster { get; set; }
        public int Metascore { get; set; }
        public decimal imdbRating { get; set; }
        public long imdbVotes { get; set; }
        public string imdbId { get; set; }
        public string type { get; set; }
        public bool Response { get; set; }
    }

    public class Movie
    {
        public string Id {get;set;}
        public string Title { get; set; }
        public int Year { get; set; }
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
    }
}
