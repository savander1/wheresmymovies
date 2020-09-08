using System.Collections.Generic;

namespace wheresmymovies.api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbImgUrl { get; set; }
        public string Description { get; set; }
        public IList<int> Years { get; set; } = new List<int>();
        public long Runtime { get; set; }
        public IList<string> Genres { get; set; } = new List<string>();
        public IList<string> Directors { get; set; } = new List<string>();
        public IList<string> Writers { get; set; } = new List<string>();
        public IList<string> Actors { get; set; } = new List<string>();
        public string FullImgUrl { get; set; }
        public IList<string> FormatLocations { get; set; } = new List<string>();
    }
}
